using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.Validation;
using EasyTools.MySqlDatabaseTools.Queries;

namespace EasyTools.MySqlDatabaseTools
{
    public interface IDatabaseCommunicator
    {
        IValidationResponse Insert(InsertQuery query);
        IValidationResponse Update(UpdateQuery query);
        IReadOnlyCollection<IReadOnlyParameterValueCollection> Select(SelectQuery query);

        IValidationResponse Delete(DeleteQuery query);
    }
}
