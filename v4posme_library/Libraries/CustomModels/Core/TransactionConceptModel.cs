﻿using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class TransactionConceptModel : ITransactionConceptModel
{
    public TbTransactionConcept GetRowByPk(int companyId, int transactionId, string? name)
    {
        using var context = new DataContext();
        return context.TbTransactionConcepts.AsNoTracking()
            .First(concept => concept.CompanyID == companyId
                              && concept.TransactionID == transactionId
                              && concept.Name == name
                              && concept.IsActive!.Value);
    }

    public List<TbTransactionConcept> GetByCompanyAndTransaction(int companyId, int transactionId)
    {
        using var context = new DataContext();
        return context.TbTransactionConcepts.AsNoTracking()
            .Where(concept => concept.CompanyID == companyId
                              && concept.TransactionID == transactionId
                              && concept.IsActive!.Value)
            .ToList();
    }
}