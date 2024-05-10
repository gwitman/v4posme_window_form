using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using v4posme_library.Libraries;

namespace v4posme_library.Models;

public partial class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DataContext()
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(s => Debug.WriteLine(s));
        var connectionString = VariablesGlobales.ConnectionString;
        optionsBuilder.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion); //Cannot find this method
    }
    public virtual DbSet<TbAccount> TbAccounts { get; set; }

    public virtual DbSet<TbAccountLevel> TbAccountLevels { get; set; }

    public virtual DbSet<TbAccountTmp> TbAccountTmps { get; set; }

    public virtual DbSet<TbAccountType> TbAccountTypes { get; set; }

    public virtual DbSet<TbAccountingBalance> TbAccountingBalances { get; set; }

    public virtual DbSet<TbAccountingBalanceTemp> TbAccountingBalanceTemps { get; set; }

    public virtual DbSet<TbAccountingCycle> TbAccountingCycles { get; set; }

    public virtual DbSet<TbAccountingPeriod> TbAccountingPeriods { get; set; }

    public virtual DbSet<TbBank> TbBanks { get; set; }

    public virtual DbSet<TbBiblia> TbBiblias { get; set; }

    public virtual DbSet<TbBranch> TbBranches { get; set; }

    public virtual DbSet<TbCaller> TbCallers { get; set; }

    public virtual DbSet<TbCashBox> TbCashBoxes { get; set; }

    public virtual DbSet<TbCashBoxSession> TbCashBoxSessions { get; set; }

    public virtual DbSet<TbCashBoxSessionTransactionMaster> TbCashBoxSessionTransactionMasters { get; set; }

    public virtual DbSet<TbCashBoxUser> TbCashBoxUsers { get; set; }

    public virtual DbSet<TbCatalog> TbCatalogs { get; set; }

    public virtual DbSet<TbCatalogItem> TbCatalogItems { get; set; }

    public virtual DbSet<TbCatalogItemConvertion> TbCatalogItemConvertions { get; set; }

    public virtual DbSet<TbCenterCost> TbCenterCosts { get; set; }

    public virtual DbSet<TbCompany> TbCompanies { get; set; }

    public virtual DbSet<TbCompanyComponent> TbCompanyComponents { get; set; }

    public virtual DbSet<TbCompanyComponentConcept> TbCompanyComponentConcepts { get; set; }

    public virtual DbSet<TbCompanyComponentFlavor> TbCompanyComponentFlavors { get; set; }

    public virtual DbSet<TbCompanyComponentItemDataview> TbCompanyComponentItemDataviews { get; set; }

    public virtual DbSet<TbCompanyCurrency> TbCompanyCurrencies { get; set; }

    public virtual DbSet<TbCompanyDataview> TbCompanyDataviews { get; set; }

    public virtual DbSet<TbCompanyDefaultDataview> TbCompanyDefaultDataviews { get; set; }

    public virtual DbSet<TbCompanyParameter> TbCompanyParameters { get; set; }

    public virtual DbSet<TbCompanySubelementAudit> TbCompanySubelementAudits { get; set; }

    public virtual DbSet<TbCompanySubelementObligatory> TbCompanySubelementObligatories { get; set; }

    public virtual DbSet<TbComponent> TbComponents { get; set; }

    public virtual DbSet<TbComponentAudit> TbComponentAudits { get; set; }

    public virtual DbSet<TbComponentAuditDetail> TbComponentAuditDetails { get; set; }

    public virtual DbSet<TbComponentAutorization> TbComponentAutorizations { get; set; }

    public virtual DbSet<TbComponentAutorizationDetail> TbComponentAutorizationDetails { get; set; }

    public virtual DbSet<TbComponentElement> TbComponentElements { get; set; }

    public virtual DbSet<TbCounter> TbCounters { get; set; }

    public virtual DbSet<TbCreditLine> TbCreditLines { get; set; }

    public virtual DbSet<TbCurrency> TbCurrencies { get; set; }

    public virtual DbSet<TbCustomer?> TbCustomers { get; set; }

    public virtual DbSet<TbCustomerConsultasSinRiesgo> TbCustomerConsultasSinRiesgoes { get; set; }

    public virtual DbSet<TbCustomerCredit> TbCustomerCredits { get; set; }

    public virtual DbSet<TbCustomerCreditAmortization> TbCustomerCreditAmortizations { get; set; }

    public virtual DbSet<TbCustomerCreditClasification> TbCustomerCreditClasifications { get; set; }

    public virtual DbSet<TbCustomerCreditDocument> TbCustomerCreditDocuments { get; set; }

    public virtual DbSet<TbCustomerCreditDocumentEntityRelated> TbCustomerCreditDocumentEntityRelateds { get; set; }

    public virtual DbSet<TbCustomerCreditExternalSharon> TbCustomerCreditExternalSharons { get; set; }

    public virtual DbSet<TbCustomerCreditExternalSharonTmp> TbCustomerCreditExternalSharonTmps { get; set; }

    public virtual DbSet<TbCustomerCreditLine> TbCustomerCreditLines { get; set; }

    public virtual DbSet<TbDataview> TbDataviews { get; set; }

    public virtual DbSet<TbElement> TbElements { get; set; }

    public virtual DbSet<TbElementType> TbElementTypes { get; set; }

    public virtual DbSet<TbEmployee> TbEmployees { get; set; }

    public virtual DbSet<TbEmployeeCalendarPay> TbEmployeeCalendarPays { get; set; }

    public virtual DbSet<TbEmployeeCalendarPayDetail> TbEmployeeCalendarPayDetails { get; set; }

    public virtual DbSet<TbEntity> TbEntities { get; set; }

    public virtual DbSet<TbEntityAccount> TbEntityAccounts { get; set; }

    public virtual DbSet<TbEntityEmail> TbEntityEmails { get; set; }

    public virtual DbSet<TbEntityPhone> TbEntityPhones { get; set; }

    public virtual DbSet<TbError> TbErrors { get; set; }

    public virtual DbSet<TbEstadisticaCategoria> TbEstadisticaCategorias { get; set; }

    public virtual DbSet<TbEstadisticaClas> TbEstadisticaClases { get; set; }

    public virtual DbSet<TbExchangeRate> TbExchangeRates { get; set; }

    public virtual DbSet<TbFixedAssent> TbFixedAssents { get; set; }

    public virtual DbSet<TbIndicator> TbIndicators { get; set; }

    public virtual DbSet<TbIndicatorHistory> TbIndicatorHistories { get; set; }

    public virtual DbSet<TbIndicatorTmp> TbIndicatorTmps { get; set; }

    public virtual DbSet<TbItem> TbItems { get; set; }

    public virtual DbSet<TbItemCategory> TbItemCategories { get; set; }

    public virtual DbSet<TbItemConfigLoto> TbItemConfigLotoes { get; set; }

    public virtual DbSet<TbItemDataSheet> TbItemDataSheets { get; set; }

    public virtual DbSet<TbItemDataSheetDetail> TbItemDataSheetDetails { get; set; }

    public virtual DbSet<TbItemImport> TbItemImports { get; set; }

    public virtual DbSet<TbItemSku> TbItemSkus { get; set; }

    public virtual DbSet<TbItemWarehouse> TbItemWarehouses { get; set; }

    public virtual DbSet<TbItemWarehouseExpired> TbItemWarehouseExpireds { get; set; }

    public virtual DbSet<TbJournalEntry> TbJournalEntries { get; set; }

    public virtual DbSet<TbJournalEntryDetail> TbJournalEntryDetails { get; set; }

    public virtual DbSet<TbJournalEntryDetailSummary> TbJournalEntryDetailSummaries { get; set; }

    public virtual DbSet<TbKardex> TbKardexes { get; set; }

    public virtual DbSet<TbLegal> TbLegals { get; set; }

    public virtual DbSet<TbListPrice> TbListPrices { get; set; }

    public virtual DbSet<TbLog> TbLogs { get; set; }

    public virtual DbSet<TbLogMesseger> TbLogMessegers { get; set; }

    public virtual DbSet<TbLogSession> TbLogSessions { get; set; }

    public virtual DbSet<TbMasterKardexTemp> TbMasterKardexTemps { get; set; }

    public virtual DbSet<TbMembership> TbMemberships { get; set; }

    public virtual DbSet<TbMenuElement> TbMenuElements { get; set; }

    public virtual DbSet<TbNaturale> TbNaturales { get; set; }

    public virtual DbSet<TbNotification> TbNotifications { get; set; }

    public virtual DbSet<TbParameter> TbParameters { get; set; }

    public virtual DbSet<TbPrice> TbPrices { get; set; }

    public virtual DbSet<TbProvider> TbProviders { get; set; }

    public virtual DbSet<TbProviderItem> TbProviderItems { get; set; }

    public virtual DbSet<TbPublicCatalog> TbPublicCatalogs { get; set; }

    public virtual DbSet<TbPublicCatalogDetail> TbPublicCatalogDetails { get; set; }

    public virtual DbSet<TbRazonesFinancierasTmp> TbRazonesFinancierasTmps { get; set; }

    public virtual DbSet<TbRelationship> TbRelationships { get; set; }

    public virtual DbSet<TbRemember> TbRemembers { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbRoleAutorization> TbRoleAutorizations { get; set; }

    public virtual DbSet<TbSubelement> TbSubelements { get; set; }

    public virtual DbSet<TbTag> TbTags { get; set; }

    public virtual DbSet<TbTransaction> TbTransactions { get; set; }

    public virtual DbSet<TbTransactionCausal> TbTransactionCausals { get; set; }

    public virtual DbSet<TbTransactionConcept> TbTransactionConcepts { get; set; }

    public virtual DbSet<TbTransactionMaster> TbTransactionMasters { get; set; }

    public virtual DbSet<TbTransactionMasterConcept> TbTransactionMasterConcepts { get; set; }

    public virtual DbSet<TbTransactionMasterDenomination> TbTransactionMasterDenominations { get; set; }

    public virtual DbSet<TbTransactionMasterDetail> TbTransactionMasterDetails { get; set; }

    public virtual DbSet<TbTransactionMasterDetailCredit> TbTransactionMasterDetailCredits { get; set; }

    public virtual DbSet<TbTransactionMasterDetailTemp> TbTransactionMasterDetailTemps { get; set; }

    public virtual DbSet<TbTransactionMasterInfo> TbTransactionMasterInfoes { get; set; }

    public virtual DbSet<TbTransactionMasterPurchase> TbTransactionMasterPurchases { get; set; }

    public virtual DbSet<TbTransactionMasterSummaryConceptTmp> TbTransactionMasterSummaryConceptTmps { get; set; }

    public virtual DbSet<TbTransactionProfileDetail> TbTransactionProfileDetails { get; set; }

    public virtual DbSet<TbTransactionProfileDetailTmp> TbTransactionProfileDetailTmps { get; set; }

    public virtual DbSet<TbTypeMenuElement> TbTypeMenuElements { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    public virtual DbSet<TbUserPermission> TbUserPermissions { get; set; }

    public virtual DbSet<TbUserTag> TbUserTags { get; set; }

    public virtual DbSet<TbUserWarehouse> TbUserWarehouses { get; set; }

    public virtual DbSet<TbWarehouse> TbWarehouses { get; set; }

    public virtual DbSet<TbWorkflow> TbWorkflows { get; set; }

    public virtual DbSet<TbWorkflowStage> TbWorkflowStages { get; set; }

    public virtual DbSet<TbWorkflowStageChangeLog> TbWorkflowStageChangeLogs { get; set; }

    public virtual DbSet<TbWorkflowStageRelation> TbWorkflowStageRelations { get; set; }

    public virtual DbSet<VwContabilidadComprobante> VwContabilidadComprobantes { get; set; }

    public virtual DbSet<VwCxcCustomerListRealEstate> VwCxcCustomerListRealEstates { get; set; }

    public virtual DbSet<VwGerenciaBalance> VwGerenciaBalances { get; set; }

    public virtual DbSet<VwGerenciaCustomer> VwGerenciaCustomers { get; set; }

    public virtual DbSet<VwGerenciaDesembolsosDetalle> VwGerenciaDesembolsosDetalles { get; set; }

    public virtual DbSet<VwGerenciaDesembolsosResuman> VwGerenciaDesembolsosResumen { get; set; }

    public virtual DbSet<VwGerenciaEstadoResultado001> VwGerenciaEstadoResultado001 { get; set; }

    public virtual DbSet<VwGerenciaEstadoResultado002> VwGerenciaEstadoResultado002 { get; set; }

    public virtual DbSet<VwInventoryListItemRealEstate> VwInventoryListItemRealEstates { get; set; }

    public virtual DbSet<VwSalesInventory> VwSalesInventories { get; set; }

    public virtual DbSet<VwSinRiesgoReporteCliente> VwSinRiesgoReporteClientes { get; set; }

    public virtual DbSet<VwSinRiesgoReporteCredito> VwSinRiesgoReporteCreditos { get; set; }

    public virtual DbSet<VwSinRiesgoReporteCreditosToSystema> VwSinRiesgoReporteCreditosToSystemas { get; set; }

    public virtual DbSet<VwTransaccionMasterConcept232425> VwTransaccionMasterConcept232425 { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<TbAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.Property(e => e.AccountNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbAccountLevel>(entity =>
        {
            entity.HasKey(e => e.AccountLevelId).HasName("PRIMARY");

            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Split).IsFixedLength();
        });

        modelBuilder.Entity<TbAccountTmp>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbAccountType>(entity =>
        {
            entity.HasKey(e => e.AccountTypeId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Naturaleza)
                .HasDefaultValueSql("'0'")
                .IsFixedLength();
        });

        modelBuilder.Entity<TbAccountingBalance>(entity =>
        {
            entity.HasKey(e => e.AccountBalanceId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbAccountingBalanceTemp>(entity =>
        {
            entity.HasKey(e => e.AccountingBalanceTempId).HasName("PRIMARY");

            entity.Property(e => e.AccountNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsOperative).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Naturaleza).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbAccountingCycle>(entity =>
        {
            entity.HasKey(e => e.ComponentCycleId).HasName("PRIMARY");

            entity.Property(e => e.EndOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Number).HasDefaultValueSql("'0'");
            entity.Property(e => e.StartOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<TbAccountingPeriod>(entity =>
        {
            entity.HasKey(e => e.ComponentPeriodId).HasName("PRIMARY");

            entity.Property(e => e.EndOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Number).HasDefaultValueSql("'0'");
            entity.Property(e => e.StartOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<TbBank>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("PRIMARY");

            entity.Property(e => e.AccountId).HasDefaultValueSql("'0'");
            entity.Property(e => e.Balance).HasDefaultValueSql("'0.00000000'");
            entity.Property(e => e.CompanyId).HasDefaultValueSql("'0'");
            entity.Property(e => e.CurrencyId).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<TbBiblia>(entity =>
        {
            entity.HasKey(e => e.VersiculoId).HasName("PRIMARY");

            entity.Property(e => e.Libro).HasDefaultValueSql("'N'");
        });

        modelBuilder.Entity<TbBranch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PRIMARY");

            entity.Property(e => e.Name).HasDefaultValueSql("'N/D'");
        });

        modelBuilder.Entity<TbCaller>(entity =>
        {
            entity.HasKey(e => e.CallerId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCashBox>(entity =>
        {
            entity.HasKey(e => e.CashBoxId).HasName("PRIMARY");

            entity.Property(e => e.CashBoxCode).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCashBoxSession>(entity =>
        {
            entity.HasKey(e => e.CashBoxSessionId).HasName("PRIMARY");

            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.StartOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<TbCashBoxSessionTransactionMaster>(entity =>
        {
            entity.HasKey(e => e.CashBoxSessionTransactionMasterId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCashBoxUser>(entity =>
        {
            entity.HasKey(e => e.CashBoxUserId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCatalog>(entity =>
        {
            entity.HasKey(e => e.CatalogId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCatalogItem>(entity =>
        {
            entity.HasKey(e => e.CatalogItemId).HasName("PRIMARY");

            entity.Property(e => e.Ratio).HasDefaultValueSql("'1.00000000'");
        });

        modelBuilder.Entity<TbCatalogItemConvertion>(entity =>
        {
            entity.HasKey(e => e.CatalogItemConvertionId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCenterCost>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PRIMARY");

            entity.Property(e => e.Number).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PRIMARY");

            entity.Property(e => e.Address).HasDefaultValueSql("'N/D'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.Name).HasDefaultValueSql("'N/D'");
            entity.Property(e => e.Type).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<TbCompanyComponent>(entity =>
        {
            entity.HasKey(e => e.CompanyComponentId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCompanyComponentConcept>(entity =>
        {
            entity.HasKey(e => e.CompanyComponentConceptId).HasName("PRIMARY");

            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCompanyComponentFlavor>(entity =>
        {
            entity.HasKey(e => e.CompanyComponentFlavorId).HasName("PRIMARY");

            entity.Property(e => e.FlavorId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCompanyComponentItemDataview>(entity =>
        {
            entity.HasKey(e => e.CompanyComponentItemDataviewId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCompanyCurrency>(entity =>
        {
            entity.HasKey(e => e.CompanyCurrencyId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCompanyDataview>(entity =>
        {
            entity.HasKey(e => e.CompanyDataViewId).HasName("PRIMARY");

            entity.Property(e => e.FlavorId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCompanyDefaultDataview>(entity =>
        {
            entity.HasKey(e => e.CompanyDefaultDataviewId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCompanyParameter>(entity =>
        {
            entity.HasKey(e => e.CompanyParameterId).HasName("PRIMARY");

            entity.Property(e => e.CustomValue).HasDefaultValueSql("'0'");
            entity.Property(e => e.Description).HasDefaultValueSql("'0'");
            entity.Property(e => e.Display).HasDefaultValueSql("'0'");
            entity.Property(e => e.Value).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCompanySubelementAudit>(entity =>
        {
            entity.HasKey(e => e.CompanySubelementAudiId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCompanySubelementObligatory>(entity =>
        {
            entity.HasKey(e => e.CompanySubelementObligatoryId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbComponent>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbComponentAudit>(entity =>
        {
            entity.HasKey(e => e.ComponentAuditId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbComponentAuditDetail>(entity =>
        {
            entity.HasKey(e => e.ComponentAuditDetailId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbComponentAutorization>(entity =>
        {
            entity.HasKey(e => e.ComponentAutorizationId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbComponentAutorizationDetail>(entity =>
        {
            entity.HasKey(e => e.ComponentAurotizationDetailId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbComponentElement>(entity =>
        {
            entity.HasKey(e => e.ComponentElementId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCounter>(entity =>
        {
            entity.HasKey(e => e.CounterId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCreditLine>(entity =>
        {
            entity.HasKey(e => e.CreditLineId).HasName("PRIMARY");

            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCurrency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("PRIMARY");

            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Simbol).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.Property(e => e.Budget).HasDefaultValueSql("'0.00'");
            entity.Property(e => e.CustomerNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.EntityContactId).HasComment("Persona que contacto al cliente");
            entity.Property(e => e.FormContactId).HasDefaultValueSql("'0'");
            entity.Property(e => e.Identification).HasDefaultValueSql("'0'");
            entity.Property(e => e.TypeFirm)
                .HasDefaultValueSql("'0'")
                .HasComment("Tipo de Firma");
        });

        modelBuilder.Entity<TbCustomerConsultasSinRiesgo>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PRIMARY");

            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("'1980-01-01 00:00:00'");
            entity.Property(e => e.File).HasDefaultValueSql("'0'");
            entity.Property(e => e.Id).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsPay).HasDefaultValueSql("b'0'");
            entity.Property(e => e.ModifiedOn).HasDefaultValueSql("'1980-01-01 00:00:00'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCustomerCredit>(entity =>
        {
            entity.HasKey(e => e.CustomerCreditId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbCustomerCreditAmortization>(entity =>
        {
            entity.HasKey(e => e.CreditAmortizationId).HasName("PRIMARY");

            entity.Property(e => e.DateApply).HasDefaultValueSql("'1980-01-01'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<TbCustomerCreditClasification>(entity =>
        {
            entity.HasKey(e => e.ClasificationId).HasName("PRIMARY");

            entity.Property(e => e.DateHistory).HasDefaultValueSql("'1980-01-01'");
        });

        modelBuilder.Entity<TbCustomerCreditDocument>(entity =>
        {
            entity.HasKey(e => e.CustomerCreditDocumentId).HasName("PRIMARY");

            entity.Property(e => e.CurrencyId).HasDefaultValueSql("'1'");
            entity.Property(e => e.DateOn).HasDefaultValueSql("'1980-01-01'");
            entity.Property(e => e.DocumentNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.ExchangeRate).HasDefaultValueSql("'1.0000'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.ReportSinRiesgo).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<TbCustomerCreditDocumentEntityRelated>(entity =>
        {
            entity.HasKey(e => e.CcEntityRelatedId).HasName("PRIMARY");

            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.RatioBalance).HasComment("Para reportar a la sin riresgo se multiplica este valor po rel Saldo");
            entity.Property(e => e.RatioBalanceExpired).HasComment("Para reportar a la sin riesgo saldo vencido");
            entity.Property(e => e.RatioDesembolso).HasComment("Para reportar a la sin riesgo se multiplica este valor por el desembolso");
            entity.Property(e => e.RatioShare).HasComment("Para reportar a la sin riego se multiplica este valor por la cuota");
            entity.Property(e => e.StatusCredit)
                .HasDefaultValueSql("'1'")
                .HasComment("Estado del Credito , Saneado, Vigente, etc");
            entity.Property(e => e.Type).HasComment("Permite saber el tipo de obligacion, DEUDOR O FIADOR");
            entity.Property(e => e.TypeCredit)
                .HasDefaultValueSql("'4'")
                .HasComment("Tipo de Credito, Consumo, Vivienda");
            entity.Property(e => e.TypeGarantia)
                .HasDefaultValueSql("'4'")
                .HasComment("Aval, Fiduciario, Pagare, etc");
            entity.Property(e => e.TypeRecuperation)
                .HasDefaultValueSql("'1'")
                .HasComment("Forma de Recuperacion Recuperacion Normal, Arreglo de pago, Cobro Extra judicial");
        });

        modelBuilder.Entity<TbCustomerCreditExternalSharon>(entity =>
        {
            entity.Property(e => e.AntiguedadDeMora).HasDefaultValueSql("'0'");
            entity.Property(e => e.Departamento).HasDefaultValueSql("'0'");
            entity.Property(e => e.Estado).HasDefaultValueSql("'0'");
            entity.Property(e => e.FechaDeDesembolso).HasDefaultValueSql("'0'");
            entity.Property(e => e.FechaDeReporte).HasDefaultValueSql("'0'");
            entity.Property(e => e.FormaDeRecuperacion).HasDefaultValueSql("'0'");
            entity.Property(e => e.FrecuenciaDePago).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'1'");
            entity.Property(e => e.NombreDePersona).HasDefaultValueSql("'0'");
            entity.Property(e => e.NumeroDeCedulaORuc).HasDefaultValueSql("'0'");
            entity.Property(e => e.NumeroDeCredito).HasDefaultValueSql("'0'");
            entity.Property(e => e.TipoDeCredito).HasDefaultValueSql("'0'");
            entity.Property(e => e.TipoDeGarantia).HasDefaultValueSql("'0'");
            entity.Property(e => e.TipoDeObligacion).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCustomerCreditExternalSharonTmp>(entity =>
        {
            entity.Property(e => e.Address).HasDefaultValueSql("'0'");
            entity.Property(e => e.CompanyName).HasDefaultValueSql("'0'");
            entity.Property(e => e.CustomerIdentification).HasDefaultValueSql("'0'");
            entity.Property(e => e.CustomerName).HasDefaultValueSql("'0'");
            entity.Property(e => e.CustomerPhone).HasDefaultValueSql("'0'");
            entity.Property(e => e.DayMora).HasDefaultValueSql("'0'");
            entity.Property(e => e.DocumentNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.FormPay).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'1'");
            entity.Property(e => e.Plazo).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbCustomerCreditLine>(entity =>
        {
            entity.HasKey(e => e.CustomerCreditLineId).HasName("PRIMARY");

            entity.Property(e => e.AccountNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.DateOpen).HasDefaultValueSql("'1980-01-01'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<TbDataview>(entity =>
        {
            entity.HasKey(e => e.DataViewId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbElement>(entity =>
        {
            entity.HasKey(e => e.ElementId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbElementType>(entity =>
        {
            entity.HasKey(e => e.ElementTypeId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbEmployee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.Property(e => e.EmployeNumber).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbEmployeeCalendarPay>(entity =>
        {
            entity.HasKey(e => e.CalendarId).HasName("PRIMARY");

            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("'1980-01-01 00:00:00'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Number).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbEmployeeCalendarPayDetail>(entity =>
        {
            entity.HasKey(e => e.CalendarDetailId).HasName("PRIMARY");

            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<TbEntity>(entity =>
        {
            entity.HasKey(e => e.EntityId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbEntityAccount>(entity =>
        {
            entity.HasKey(e => e.EntityAccountId).HasName("PRIMARY");

            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("'1980-01-01 00:00:00'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbEntityEmail>(entity =>
        {
            entity.HasKey(e => e.EntityEmailId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbEntityPhone>(entity =>
        {
            entity.HasKey(e => e.EntityPhoneId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbError>(entity =>
        {
            entity.HasKey(e => e.ErrorId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbEstadisticaCategoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PRIMARY");

            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbEstadisticaClas>(entity =>
        {
            entity.HasKey(e => e.ClaseId).HasName("PRIMARY");

            entity.Property(e => e.IsActive).HasDefaultValueSql("b'1'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbExchangeRate>(entity =>
        {
            entity.HasKey(e => e.ExchangeRateId).HasName("PRIMARY");

            entity.Property(e => e.Date).HasDefaultValueSql("'1980-01-01'");
        });

        modelBuilder.Entity<TbFixedAssent>(entity =>
        {
            entity.HasKey(e => e.FixedAssentId).HasName("PRIMARY");

            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<TbIndicator>(entity =>
        {
            entity.HasKey(e => e.IndicadorId).HasName("PRIMARY");

            entity.Property(e => e.Code).HasDefaultValueSql("'0'");
            entity.Property(e => e.Description).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsGroup).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Label).HasDefaultValueSql("'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Posfix).HasDefaultValueSql("'0'");
            entity.Property(e => e.Prefix).HasDefaultValueSql("'0'");
            entity.Property(e => e.Script).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbIndicatorHistory>(entity =>
        {
            entity.HasKey(e => e.IndicatorHistoryId).HasName("PRIMARY");

            entity.Property(e => e.DateOn).HasDefaultValueSql("'1980-01-01'");
        });

        modelBuilder.Entity<TbIndicatorTmp>(entity =>
        {
            entity.HasKey(e => e.IndicatorTmpId).HasName("PRIMARY");

            entity.Property(e => e.TokenId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.HasIndex(e => e.BarCode, "IDX_ITEM_006").HasAnnotation("MySql:IndexPrefixLength", new[] { 1000 });

            entity.Property(e => e.CurrencyId).HasDefaultValueSql("'1'");
            entity.Property(e => e.IsInvoice).HasDefaultValueSql("'1'");
            entity.Property(e => e.IsInvoiceQuantityZero).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsServices).HasDefaultValueSql("'0'");
            entity.Property(e => e.ItemNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStateAceptanMascota).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStateClubPiscina).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStateContractCorrentaje).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStateGerenciaExclusive).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStatePiscinaPrivate).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStatePlanReference).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStateRooBatchVisit).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStateRoomBatchServices).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStateRoomServices).HasDefaultValueSql("'0'");
            entity.Property(e => e.RealStateWallInCloset).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbItemCategory>(entity =>
        {
            entity.HasKey(e => e.InventoryCategoryId).HasName("PRIMARY");

            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbItemConfigLoto>(entity =>
        {
            entity.HasKey(e => e.ItemConfigLotoId).HasName("PRIMARY");

            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.ItemId).HasDefaultValueSql("'1'");
            entity.Property(e => e.MaxSale).HasDefaultValueSql("'1.00'");
            entity.Property(e => e.Turno1Fin).HasDefaultValueSql("'9'");
            entity.Property(e => e.Turno2Fin).HasDefaultValueSql("'14'");
            entity.Property(e => e.Turno2Inicio).HasDefaultValueSql("'9'");
            entity.Property(e => e.Turno3Fin).HasDefaultValueSql("'22'");
            entity.Property(e => e.Turno3Inicio).HasDefaultValueSql("'14'");
        });

        modelBuilder.Entity<TbItemDataSheet>(entity =>
        {
            entity.HasKey(e => e.ItemDataSheetId).HasName("PRIMARY");

            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("'1980-01-01 00:00:00'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbItemDataSheetDetail>(entity =>
        {
            entity.HasKey(e => e.ItemDataSheetDetailId).HasName("PRIMARY");

            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<TbItemSku>(entity =>
        {
            entity.HasKey(e => e.SkuId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbItemWarehouse>(entity =>
        {
            entity.HasKey(e => e.ItemWarehouseId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbItemWarehouseExpired>(entity =>
        {
            entity.HasKey(e => e.ItemWarehouseExpiredId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbJournalEntry>(entity =>
        {
            entity.HasKey(e => e.JournalEntryId).HasName("PRIMARY");

            entity.Property(e => e.EntryName).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsModule).HasDefaultValueSql("b'0'");
            entity.Property(e => e.JournalDate).HasDefaultValueSql("'1980-01-01'");
            entity.Property(e => e.JournalNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.TitleTemplated).HasDefaultValueSql("'N/A'");
        });

        modelBuilder.Entity<TbJournalEntryDetail>(entity =>
        {
            entity.HasKey(e => e.JournalEntryDetailId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbJournalEntryDetailSummary>(entity =>
        {
            entity.HasKey(e => e.JournalEntryDetailSummaryId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbKardex>(entity =>
        {
            entity.HasKey(e => e.KardexId).HasName("PRIMARY");

            entity.Property(e => e.MovementOn).HasDefaultValueSql("'1980-01-01 00:00:00'");
        });

        modelBuilder.Entity<TbLegal>(entity =>
        {
            entity.HasKey(e => e.LegalId).HasName("PRIMARY");

            entity.Property(e => e.Address).HasDefaultValueSql("'N/D'");
            entity.Property(e => e.ComercialName).HasDefaultValueSql("'N/D'");
            entity.Property(e => e.LegalName).HasDefaultValueSql("'N/D'");
        });

        modelBuilder.Entity<TbListPrice>(entity =>
        {
            entity.HasKey(e => e.ListPriceId).HasName("PRIMARY");

            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("'1980-01-01 00:00:00'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.StartOn).HasDefaultValueSql("'1980-01-01 00:00:00'");
        });

        modelBuilder.Entity<TbLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Description).HasDefaultValueSql("'0'");
            entity.Property(e => e.ProcedureName).HasDefaultValueSql("'0'");
            entity.Property(e => e.Token).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbLogMesseger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IpAddress).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbLogSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PRIMARY");

            entity.Property(e => e.SessionId).HasDefaultValueSql("'0'");
            entity.Property(e => e.IpAddress).HasDefaultValueSql("'0'");
            entity.Property(e => e.LastActivity).HasDefaultValueSql("'0'");
            entity.Property(e => e.UserAgent).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbMasterKardexTemp>(entity =>
        {
            entity.HasKey(e => e.MasterKardexTempId).HasName("PRIMARY");

            entity.Property(e => e.ItemName).HasDefaultValueSql("'0'");
            entity.Property(e => e.ItemNumber).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbMembership>(entity =>
        {
            entity.HasKey(e => e.MembershipId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbMenuElement>(entity =>
        {
            entity.HasKey(e => e.MenuElementId).HasName("PRIMARY");

            entity.Property(e => e.Icon).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.Template).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbNaturale>(entity =>
        {
            entity.HasKey(e => e.NaturalesId).HasName("PRIMARY");

            entity.Property(e => e.ProfesionId).HasComment("Catalogo de Profesion u Oficio");
            entity.Property(e => e.StatusId).HasComment("Catalogo de Estado Civil");
        });

        modelBuilder.Entity<TbNotification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PRIMARY");

            entity.ToTable("tb_notification", tb => tb.HasComment("Tabla de Notificaciones"));

            entity.Property(e => e.QuantityDisponible).HasDefaultValueSql("'0'");
            entity.Property(e => e.QuantityOcupation).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbParameter>(entity =>
        {
            entity.HasKey(e => e.ParameterId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbPrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbProvider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PRIMARY");

            entity.Property(e => e.ProviderNumber).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbProviderItem>(entity =>
        {
            entity.HasKey(e => e.ProviderItemId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbPublicCatalog>(entity =>
        {
            entity.HasKey(e => e.PublicCatalogId).HasName("PRIMARY");

            entity.Property(e => e.FlavorId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbPublicCatalogDetail>(entity =>
        {
            entity.HasKey(e => e.PublicCatalogDetailId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbRazonesFinancierasTmp>(entity =>
        {
            entity.HasKey(e => e.RzId).HasName("PRIMARY");

            entity.Property(e => e.Description).HasDefaultValueSql("'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Sequence).HasDefaultValueSql("'0'");
            entity.Property(e => e.Simbol).HasDefaultValueSql("'0'");
            entity.Property(e => e.Token).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbRelationship>(entity =>
        {
            entity.HasKey(e => e.RelationshipId).HasName("PRIMARY");

            entity.Property(e => e.EndOn).HasDefaultValueSql("'1980-01-01'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.StartOn).HasDefaultValueSql("'1980-01-01'");
        });

        modelBuilder.Entity<TbRemember>(entity =>
        {
            entity.HasKey(e => e.RememberId).HasName("PRIMARY");

            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Description).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsTemporal).HasDefaultValueSql("b'0'");
            entity.Property(e => e.LeerFile).HasDefaultValueSql("'0'");
            entity.Property(e => e.TagId).HasDefaultValueSql("'0'");
            entity.Property(e => e.Title).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.Property(e => e.UrlDefault).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbRoleAutorization>(entity =>
        {
            entity.HasKey(e => e.RoleAurotizationId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbSubelement>(entity =>
        {
            entity.HasKey(e => e.SubElementId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbTag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PRIMARY");

            entity.ToTable("tb_tag", tb => tb.HasComment("tabla para almacenar los tag de notificaciones"));
        });

        modelBuilder.Entity<TbTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PRIMARY");

            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbTransactionCausal>(entity =>
        {
            entity.HasKey(e => e.TransactionCausalId).HasName("PRIMARY");

            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.IsDefault).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbTransactionConcept>(entity =>
        {
            entity.HasKey(e => e.ConceptId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbTransactionMaster>(entity =>
        {
            entity.HasKey(e => e.TransactionMasterId).HasName("PRIMARY");

            entity.Property(e => e.DescriptionReference).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsTemplate).HasDefaultValueSql("'0'");
            entity.Property(e => e.PrinterQuantity).HasDefaultValueSql("'0'");
            entity.Property(e => e.TransactionNumber).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbTransactionMasterConcept>(entity =>
        {
            entity.HasKey(e => e.TransactionMasterConceptId).HasName("PRIMARY");

            entity.Property(e => e.Value).HasDefaultValueSql("'0.0000'");
        });

        modelBuilder.Entity<TbTransactionMasterDenomination>(entity =>
        {
            entity.HasKey(e => e.TransactionMasterDenominationId).HasName("PRIMARY");

            entity.Property(e => e.CatalogItemId).HasDefaultValueSql("'1'");
            entity.Property(e => e.CompanyId).HasDefaultValueSql("'1'");
            entity.Property(e => e.ComponentId).HasDefaultValueSql("'1'");
            entity.Property(e => e.CurrencyId).HasDefaultValueSql("'1'");
            entity.Property(e => e.ExchangeRate).HasDefaultValueSql("'1.00000000'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.Quantity).HasDefaultValueSql("'1'");
            entity.Property(e => e.Ratio).HasDefaultValueSql("'1.00000000'");
            entity.Property(e => e.TransactionId).HasDefaultValueSql("'1'");
            entity.Property(e => e.TransactionMasterId).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<TbTransactionMasterDetail>(entity =>
        {
            entity.HasKey(e => e.TransactionMasterDetailId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbTransactionMasterDetailCredit>(entity =>
        {
            entity.HasKey(e => e.TransactionMasterDetailCreditId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbTransactionMasterDetailTemp>(entity =>
        {
            entity.HasKey(e => e.TransactionMasterDetailTemporalId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbTransactionMasterInfo>(entity =>
        {
            entity.HasKey(e => e.TransactionMasterInfoId).HasName("PRIMARY");

            entity.Property(e => e.ReceiptAmountBankDolId).HasDefaultValueSql("'0'");
            entity.Property(e => e.ReceiptAmountBankId).HasDefaultValueSql("'0'");
            entity.Property(e => e.ReceiptAmountCardBankDolId).HasDefaultValueSql("'0'");
            entity.Property(e => e.ReceiptAmountCardBankId).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbTransactionMasterPurchase>(entity =>
        {
            entity.HasKey(e => e.TransactionMasterPurchaseId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbTransactionMasterSummaryConceptTmp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbTransactionProfileDetail>(entity =>
        {
            entity.HasKey(e => e.ProfileDetailId).HasName("PRIMARY");

            entity.Property(e => e.AccountId).HasDefaultValueSql("'0'");
            entity.Property(e => e.Sign)
                .HasDefaultValueSql("'0'")
                .IsFixedLength();
        });

        modelBuilder.Entity<TbTransactionProfileDetailTmp>(entity =>
        {
            entity.HasKey(e => e.TransactionProfileDetailTmpId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbTypeMenuElement>(entity =>
        {
            entity.HasKey(e => e.TypeMenuElementId).HasName("PRIMARY");

            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.Property(e => e.Email).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbUserPermission>(entity =>
        {
            entity.HasKey(e => e.UserPermissionId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbUserTag>(entity =>
        {
            entity.HasKey(e => e.UserTagId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbUserWarehouse>(entity =>
        {
            entity.HasKey(e => e.UserWarehouseId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbWarehouse>(entity =>
        {
            entity.HasKey(e => e.WarehouseId).HasName("PRIMARY");

            entity.Property(e => e.CreatedIn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("'1980-01-01 00:00:00'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("b'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Number).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<TbWorkflow>(entity =>
        {
            entity.HasKey(e => e.WorkflowId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbWorkflowStage>(entity =>
        {
            entity.HasKey(e => e.WorkflowStageId).HasName("PRIMARY");

            entity.Property(e => e.Aplicable).HasComment("Este campo es util para saber si el documento debe de aumentar o disminuir inventario o para saver si el documento debe de ser contabilizado");
        });

        modelBuilder.Entity<TbWorkflowStageChangeLog>(entity =>
        {
            entity.HasKey(e => e.WorkflowStageChangeLogId).HasName("PRIMARY");
        });

        modelBuilder.Entity<TbWorkflowStageRelation>(entity =>
        {
            entity.HasKey(e => e.WorkflowStageRelationId).HasName("PRIMARY");
        });

        modelBuilder.Entity<VwContabilidadComprobante>(entity =>
        {
            entity.ToView("vw_contabilidad_comprobantes");

            entity.Property(e => e.BeneficiarioComprobante).HasDefaultValueSql("'0'");
            entity.Property(e => e.CodigoComprobante).HasDefaultValueSql("'0'");
            entity.Property(e => e.CodigoCuenta).HasDefaultValueSql("''");
            entity.Property(e => e.FechaComprobante).HasDefaultValueSql("'1980-01-01'");
            entity.Property(e => e.MonedaComprobante).HasDefaultValueSql("'0'");
            entity.Property(e => e.NombreCuenta).HasDefaultValueSql("'0'");
            entity.Property(e => e.TipoCuenta).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<VwCxcCustomerListRealEstate>(entity =>
        {
            entity.ToView("vw_cxc_customer_list_real_estate");

            entity.Property(e => e.Agente).HasDefaultValueSql("''");
            entity.Property(e => e.Codigo).HasDefaultValueSql("'0'");
            entity.Property(e => e.Presupuesto).HasDefaultValueSql("'0.00'");
        });

        modelBuilder.Entity<VwGerenciaBalance>(entity =>
        {
            entity.ToView("vw_gerencia_balance");

            entity.Property(e => e.Cuenta).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<VwGerenciaCustomer>(entity =>
        {
            entity.ToView("vw_gerencia_customer");

            entity.Property(e => e.CustomerNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.Identification).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<VwGerenciaDesembolsosDetalle>(entity =>
        {
            entity.ToView("vw_gerencia_desembolsos_detalle");

            entity.Property(e => e.Cliente).HasDefaultValueSql("'0'");
            entity.Property(e => e.Colaborador).HasDefaultValueSql("'0'");
            entity.Property(e => e.Factura).HasDefaultValueSql("'0'");
            entity.Property(e => e.FechaCuota).HasDefaultValueSql("'1980-01-01'");
            entity.Property(e => e.Moneda).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<VwGerenciaDesembolsosResuman>(entity =>
        {
            entity.ToView("vw_gerencia_desembolsos_resumen");

            entity.Property(e => e.CodigoCliente).HasDefaultValueSql("'0'");
            entity.Property(e => e.Factura).HasDefaultValueSql("'0'");
            entity.Property(e => e.Fecha).HasDefaultValueSql("'1980-01-01'");
            entity.Property(e => e.Moneda).HasDefaultValueSql("'0'");
            entity.Property(e => e.TipoCambio).HasDefaultValueSql("'1.0000'");
        });

        modelBuilder.Entity<VwGerenciaEstadoResultado001>(entity =>
        {
            entity.ToView("vw_gerencia_estado_resultado_001");

            entity.Property(e => e.Cuenta).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<VwGerenciaEstadoResultado002>(entity =>
        {
            entity.ToView("vw_gerencia_estado_resultado_002");
        });

        modelBuilder.Entity<VwInventoryListItemRealEstate>(entity =>
        {
            entity.ToView("vw_inventory_list_item_real_estate");

            entity.Property(e => e.AceptaMascota).HasDefaultValueSql("''");
            entity.Property(e => e.Agente).HasDefaultValueSql("''");
            entity.Property(e => e.Amueblado).HasDefaultValueSql("''");
            entity.Property(e => e.AreaClubConPiscina).HasDefaultValueSql("''");
            entity.Property(e => e.BañoDeServicio).HasDefaultValueSql("''");
            entity.Property(e => e.BañoDeVisita).HasDefaultValueSql("''");
            entity.Property(e => e.Codigo).HasDefaultValueSql("'0'");
            entity.Property(e => e.Corretaje).HasDefaultValueSql("''");
            entity.Property(e => e.CuartoDeServicio).HasDefaultValueSql("''");
            entity.Property(e => e.DiseñoDePropiedad).HasDefaultValueSql("'0'");
            entity.Property(e => e.Disponible).HasDefaultValueSql("''");
            entity.Property(e => e.ItemId).HasDefaultValueSql("''");
            entity.Property(e => e.Moneda).HasDefaultValueSql("'0'");
            entity.Property(e => e.Nombre).HasDefaultValueSql("'0'");
            entity.Property(e => e.PiscinaPrivada).HasDefaultValueSql("''");
            entity.Property(e => e.PlanDeReferido).HasDefaultValueSql("''");
            entity.Property(e => e.WalkInCloset).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<VwSalesInventory>(entity =>
        {
            entity.ToView("vw_sales_inventory");

            entity.Property(e => e.CategoryName).HasDefaultValueSql("'0'");
            entity.Property(e => e.Causal).HasDefaultValueSql("'0'");
            entity.Property(e => e.CompaniaName).HasDefaultValueSql("'N/D'");
            entity.Property(e => e.Currency).HasDefaultValueSql("'0'");
            entity.Property(e => e.CustomerNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.ItemNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.Name).HasDefaultValueSql("'0'");
            entity.Property(e => e.Tipo).HasDefaultValueSql("'0'");
            entity.Property(e => e.TransactionNumber).HasDefaultValueSql("'0'");
            entity.Property(e => e.WarehouseName).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<VwSinRiesgoReporteCliente>(entity =>
        {
            entity.ToView("vw_sin_riesgo_reporte_clientes");

            entity.Property(e => e.ActividadEconomica).HasDefaultValueSql("''");
            entity.Property(e => e.CorreoElectronico)
                .HasDefaultValueSql("''")
                .IsFixedLength();
            entity.Property(e => e.Departamento).HasDefaultValueSql("''");
            entity.Property(e => e.DepartamentoDeTrabajo).HasDefaultValueSql("''");
            entity.Property(e => e.EstadoCivil).HasDefaultValueSql("''");
            entity.Property(e => e.Identificacion).HasDefaultValueSql("''");
            entity.Property(e => e.Municipio).HasDefaultValueSql("''");
            entity.Property(e => e.MunicipioDeTrabajo).HasDefaultValueSql("''");
            entity.Property(e => e.Nacionalidad).HasDefaultValueSql("''");
            entity.Property(e => e.Ocupacion).HasDefaultValueSql("''");
            entity.Property(e => e.Sector).HasDefaultValueSql("''");
            entity.Property(e => e.Sexo).HasDefaultValueSql("''");
            entity.Property(e => e.TipoDePersona).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<VwSinRiesgoReporteCredito>(entity =>
        {
            entity.ToView("vw_sin_riesgo_reporte_creditos");

            entity.Property(e => e.CompanyId).HasDefaultValueSql("'0'");
            entity.Property(e => e.CustomerCreditDocumentId).HasDefaultValueSql("'0'");
            entity.Property(e => e.Departamento).HasDefaultValueSql("''");
            entity.Property(e => e.EntityId).HasDefaultValueSql("'0'");
            entity.Property(e => e.FrecuenciaDePago).HasDefaultValueSql("''");
            entity.Property(e => e.NumeroCorrelativo).HasDefaultValueSql("''");
            entity.Property(e => e.NumeroDeCedulaORuc).HasDefaultValueSql("''");
            entity.Property(e => e.NumeroDeCredito).HasDefaultValueSql("'0'");
            entity.Property(e => e.TipoDeEntidad).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<VwSinRiesgoReporteCreditosToSystema>(entity =>
        {
            entity.ToView("vw_sin_riesgo_reporte_creditos_to_systema");

            entity.Property(e => e.CompanyId).HasDefaultValueSql("'0'");
            entity.Property(e => e.Departamento).HasDefaultValueSql("''");
            entity.Property(e => e.FrecuenciaDePago).HasDefaultValueSql("''");
            entity.Property(e => e.NumeroCorrelativo).HasDefaultValueSql("''");
            entity.Property(e => e.NumeroDeCedulaORuc).HasDefaultValueSql("''");
            entity.Property(e => e.NumeroDeCredito).HasDefaultValueSql("'0'");
            entity.Property(e => e.TipoDeEntidad).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<VwTransaccionMasterConcept232425>(entity =>
        {
            entity.ToView("vw_transaccion_master_concept_232425");

            entity.Property(e => e.Descripcion).HasDefaultValueSql("'0'");
            entity.Property(e => e.Documento).HasDefaultValueSql("'0'");
            entity.Property(e => e.Moneda).HasDefaultValueSql("'0'");
            entity.Property(e => e.Valor).HasDefaultValueSql("'0.0000'");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
