//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.Infrastructure.MappingViews;

[assembly: DbMappingViewCacheTypeAttribute(
    typeof(TaskManagement.Models.TestEntities),
    typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets84571e5a6316ef97d89a899dfc7f0a863a54f9cb5cf093f380c4f60bd9a831e9))]

namespace Edm_EntityMappingGeneratedViews
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Core.Metadata.Edm;

    /// <summary>
    /// Implements a mapping view cache.
    /// </summary>
    [GeneratedCode("Entity Framework 6 Power Tools", "0.9.5.0")]
    internal sealed class ViewsForBaseEntitySets84571e5a6316ef97d89a899dfc7f0a863a54f9cb5cf093f380c4f60bd9a831e9 : DbMappingViewCache
    {
        /// <summary>
        /// Gets a hash value computed over the mapping closure.
        /// </summary>
        public override string MappingHashValue
        {
            get { return "84571e5a6316ef97d89a899dfc7f0a863a54f9cb5cf093f380c4f60bd9a831e9"; }
        }

        /// <summary>
        /// Gets a view corresponding to the specified extent.
        /// </summary>
        /// <param name="extent">The extent.</param>
        /// <returns>The mapping view, or null if the extent is not associated with a mapping view.</returns>
        public override DbMappingView GetView(EntitySetBase extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException("extent");
            }

            var extentName = extent.EntityContainer.Name + "." + extent.Name;

            if (extentName == "TestModelStoreContainer.tb_task")
            {
                return GetView0();
            }

            if (extentName == "TestEntities.tb_task")
            {
                return GetView1();
            }

            return null;
        }

        /// <summary>
        /// Gets the view for TestModelStoreContainer.tb_task.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView0()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing tb_task
        [TestModel.Store.tb_task](T1.[tb_task.task_id], T1.[tb_task.task_name], T1.[tb_task.task_description], T1.[tb_task.due_date], T1.[tb_task.status], T1.[tb_task.priority])
    FROM (
        SELECT 
            T.task_id AS [tb_task.task_id], 
            T.task_name AS [tb_task.task_name], 
            T.task_description AS [tb_task.task_description], 
            T.due_date AS [tb_task.due_date], 
            T.status AS [tb_task.status], 
            T.priority AS [tb_task.priority], 
            True AS _from0
        FROM TestEntities.tb_task AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for TestEntities.tb_task.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView1()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing tb_task
        [TestModel.tb_task](T1.[tb_task.task_id], T1.[tb_task.task_name], T1.[tb_task.task_description], T1.[tb_task.due_date], T1.[tb_task.status], T1.[tb_task.priority])
    FROM (
        SELECT 
            T.task_id AS [tb_task.task_id], 
            T.task_name AS [tb_task.task_name], 
            T.task_description AS [tb_task.task_description], 
            T.due_date AS [tb_task.due_date], 
            T.status AS [tb_task.status], 
            T.priority AS [tb_task.priority], 
            True AS _from0
        FROM TestModelStoreContainer.tb_task AS T
    ) AS T1");
        }
    }
}
