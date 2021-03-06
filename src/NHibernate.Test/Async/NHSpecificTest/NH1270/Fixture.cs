﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Text;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1270
{
	using System.Threading.Tasks;
	using System.Threading;
	[TestFixture]
	public class FixtureAsync
	{
		private HbmMapping GetMappings()
		{
			var mapper = new ModelMapper();
			mapper.Class<User>(rt =>
							   {
								rt.Id(x => x.Id, map => map.Generator(Generators.Guid));
								rt.Property(x => x.Name);
													rt.Set(x => x.Roles, map =>
																							 {
																								 map.Table("UsersToRoles");
																								 map.Inverse(true);
																								 map.Key(km => km.Column("UserId"));
																							 }, rel => rel.ManyToMany(mm =>
																													  {
																																					mm.Column("RoleId");
																														mm.ForeignKey("FK_RoleInUser");
																													  }));
							   });
			mapper.Class<Role>(rt =>
							   {
								rt.Id(x => x.Id, map => map.Generator(Generators.Guid));
								rt.Property(x => x.Name);
								rt.Set(x => x.Users, map =>
													 {
														map.Table("UsersToRoles");
																								map.Key(km => km.Column("RoleId"));
																							 }, rel => rel.ManyToMany(mm =>
																													  {
																																					mm.Column("UserId");
																														mm.ForeignKey("FK_UserInRole");
																													  }));
							   });
			var mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();
			return mappings;
		}

		[Test]
		public Task WhenMapCustomFkNamesThenUseItAsync()
		{
			try
			{
				var conf = TestConfigurationHelper.GetDefaultConfiguration();
				conf.DataBaseIntegration(i=> i.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote);
				conf.AddMapping(GetMappings());
				var sb = new StringBuilder();
				(new SchemaExport(conf)).Create(s => sb.AppendLine(s), true);

				Assert.That(sb.ToString(), Does.Contain("FK_RoleInUser").And.Contains("FK_UserInRole"));
				return (new SchemaExport(conf)).DropAsync(false, true);
			}
			catch (System.Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}
	}
}
