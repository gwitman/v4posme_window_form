using System.Collections;
using System.Data;
using DevExpress.Data;
using DevExpress.XtraEditors;
using System.Globalization;
using DevExpress.Data.Helpers;
using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.Utils;

namespace v4posme_window.Libraries;

public class VirtualPagerHelper : IDisposable
{
    public static class HtmlPagerHelper
    {
        public static IEnumerable<string> GenerateHtmlPagerElements(int currentPage, int pages, int visibleRadius, string lastPageTextOverride)
        {
            for (int pg = 0; pg < pages; ++pg)
            {
                if (pg == visibleRadius + 1 && pg < currentPage - visibleRadius - 1)
                {
                    pg = currentPage - visibleRadius;
                    yield return "…";
                }
                if (pg == currentPage + visibleRadius + 1 && pg < pages - visibleRadius - 2)
                {
                    pg = pages - visibleRadius - 1;
                    yield return "…";
                }
                var pageDispText = (lastPageTextOverride != null && pg != currentPage && pg == pages - 1) ? lastPageTextOverride : (pg + 1).ToString(CultureInfo.InvariantCulture);
                if (pg == currentPage)
                    yield return "<b>" + pageDispText + "</b>";
                else
                    yield return "<href=" + pg + ">" + pageDispText + "</href>";
            }
        }
        public static string GenerateHtmlPager(int currentPage, int pages, int visibleRadius, string lastPageTextOverride)
        {
            return string.Join(" ", GenerateHtmlPagerElements(currentPage, pages, visibleRadius, lastPageTextOverride));
        }
        public static int DefaultVisibleRadius = 4;
        public static string GenerateHtmlPager(int currentPage, int pages)
        {
            return string.Join(" ", GenerateHtmlPagerElements(currentPage, pages, DefaultVisibleRadius, null));
        }
        public const string MoreButtonText = "→";
        public static string GenerateHtmlPager(int currentPage, int pages, string lastPageTextOverride)
        {
            return string.Join(" ", GenerateHtmlPagerElements(currentPage, pages, DefaultVisibleRadius, lastPageTextOverride));
        }
    }
    int _pageSize;
    int _currentPage;
    int _pagesCount;
    bool _pagesCountKnownExactly;
    CancellationTokenSource _pagesCountCancellation;
    VirtualServerModeSource _virtualSource;
    LabelControl _htmlPager;
    Func<VirtualServerModeConfigurationInfo, int, int, CancellationToken, Task<DataTable>> _rowsTask;
    Func<VirtualServerModeConfigurationInfo, CancellationToken, Task<int>> _rowCountTask;
    public VirtualPagerHelper(int initialPageSize, VirtualServerModeSource virtualSource,
        LabelControl htmlPager,
        Func<VirtualServerModeConfigurationInfo, int, int, CancellationToken, Task<DataTable>> rowsTask,
        Func<VirtualServerModeConfigurationInfo, CancellationToken, Task<int>> rowCountTask)
    {
        _pageSize = initialPageSize;
        _virtualSource = virtualSource;
        _htmlPager = htmlPager;
        _rowsTask = rowsTask;
        _rowCountTask = rowCountTask;
        _pagesCount = 1;
        _pagesCountKnownExactly = false;
        _currentPage = 0;
        htmlPager.HyperlinkClick += HtmlPager_HyperlinkClick;
        virtualSource.ConfigurationChanged += src_ConfigurationChanged!;
        virtualSource.MoreRows += src_MoreRows!;
        UpdatePager();
    }
    public void Dispose()
    {
        if (_htmlPager != null)
            _htmlPager.HyperlinkClick -= HtmlPager_HyperlinkClick;
        _htmlPager = null;
        if (_virtualSource != null)
        {
            _virtualSource.ConfigurationChanged -= src_ConfigurationChanged!;
            _virtualSource.MoreRows -= src_MoreRows;
            _htmlPager = null;
        }
        CancelPagesTask();
    }
    void CancelPagesTask()
    {
        if (_pagesCountCancellation == null)
            return;
        var c = _pagesCountCancellation;
        _pagesCountCancellation = null;
        c.Cancel();
        c.Dispose();
    }

    private async Task<VirtualServerModeRowsTaskResult> GetRowsPageAsync(VirtualServerModeRowsEventArgs e)
    {
        var userTask = _rowsTask(e.ConfigurationInfo, _pageSize, _currentPage, e.CancellationToken);
        var userRv = await userTask;
        OnPageLoaded(userRv.Rows.Count);
        return new VirtualServerModeRowsTaskResult(userRv.Rows);
    }

    private void src_MoreRows(object sender, VirtualServerModeRowsEventArgs e)
    {
        e.RowsTask = GetRowsPageAsync(e);
    }

    private void src_ConfigurationChanged(object sender, VirtualServerModeRowsEventArgs e)
    {
        if (inPageClick != 0)    // pager navigation, not real reconfiguration
            return;
        CancelPagesTask();
        _currentPage = 0;
        _pagesCount = 1;
        _pagesCountKnownExactly = false;
        UpdatePager();
        var pagesTask = GoPagesCount(e);
    }
    async Task GoPagesCount(VirtualServerModeRowsEventArgs e)
    {
        if (_rowCountTask == null)
            return;
        var cancellationSource = new CancellationTokenSource();
        this._pagesCountCancellation = cancellationSource;
        int rowCount = await _rowCountTask(e.ConfigurationInfo, cancellationSource.Token);
        CancelPagesTask();
        if (rowCount >= 0)
        {
            _pagesCountKnownExactly = true;
            _pagesCount = (rowCount + _pageSize - 1) / _pageSize;
            UpdatePager();
        }
    }
    void HtmlPager_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
    {
        OnPageClick(int.Parse(e.Link, CultureInfo.InvariantCulture));
    }
    int inPageClick;
    void OnPageClick(int page)
    {
        ++inPageClick;
        try
        {
            _currentPage = page;
            UpdatePager();
            _virtualSource.Refresh();
        }
        finally { --inPageClick; }
    }
    public void SetPageSize(int newPageSize)
    {
        _pageSize = newPageSize;
        _currentPage = 0;
        _virtualSource.Refresh();
    }
    void OnPageLoaded(int rows)
    {
        if (!_pagesCountKnownExactly)
        {
            if (_currentPage == _pagesCount - 1)
            {
                if (rows < _pageSize)
                {
                    CancelPagesTask();
                    _pagesCountKnownExactly = true;
                    if (rows == 0)
                        _pagesCount--;
                }
                else _pagesCount++;
            }
        }
        UpdatePager();
    }
    void UpdatePager()
    {
        if (_pagesCountKnownExactly)
            _htmlPager.Text = HtmlPagerHelper.GenerateHtmlPager(_currentPage, Math.Max(_pagesCount, _currentPage + 1));
        else
            _htmlPager.Text = HtmlPagerHelper.GenerateHtmlPager(_currentPage, _pagesCount, HtmlPagerHelper.MoreButtonText);
    }
}
