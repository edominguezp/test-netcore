using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tanner.Core.API.Infrastructure.Configurations;
using Tanner.Core.API.Interfaces;
using Tanner.Core.DataAccess;
using Tanner.Core.DataAccess.Commands;
using Tanner.Core.DataAccess.Configurations;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.Core.Service.Implementations;
using Tanner.Core.Service.Interfaces;
using Tanner.RelationalDataAccess;
using Tanner.RestClient;

namespace Tanner.Core.API.Infrastructure
{
    /// <summary>
    /// Initialize API
    /// </summary>
    public static class APIInitialize
    {
        private const string DBKEY_Core = "Core";
        private const string DBKEY_CoreFile = "Core-File";
        private const string DBKEY_Intelicom = "Intelicom";
        private const string DBKEY_Segpriv = "Segpriv";


        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void InitializeAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            
            services.AddDbContext(configuration);

            services.AddConfigurations(configuration);

            services.AddClients(configuration);

            services.AddCustomServices();

        }
        
        /// <summary>
        /// Add repositories to scope
        /// </summary>
        /// <param name="services"></param>
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<ICommercialHierarchyRepository, CommercialHierarchyRepository>();

            services.AddScoped<IOperationRepository, OperationRepository>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IZoneRepository, ZoneRepository>();

            services.AddScoped<IFileRepository, FileRepository>();

            services.AddScoped<ICoreRepository, CoreRepository>();

            services.AddScoped<IIntelicomRepository, IntelicomRepository>();

            services.AddScoped<IRegionRepository, RegionRepository>();

            services.AddScoped<IRatificationDocumentRepository, RatificationDocumentRepository>();

            services.AddScoped<IDebtorRepository, DebtorRepository>();

            services.AddScoped<IDocumentRepository, DocumentRepository>();

            services.AddScoped<IBankRepository, BankRepository> ();

            services.AddScoped<IDocumentsDebtorRepository, DocumentsDebtorRepository>();

            services.AddScoped<ISimulationRepository, SimulationRepository>();

            services.AddScoped<IBranchOfficeRepository, BranchOfficeRepository>();

            services.AddScoped<IChangeTypeRepository, ChangeTypeRepository>();

            services.AddScoped<IChannelRepository, ChannelRepository>();

            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();

            services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();

            services.AddScoped<IAssignmentContractRepository, AssignmentContractRepository>();

            services.AddScoped<ILogRepository, LogRepository>();

            services.AddScoped<ISegprivUserRepository, SegprivUserRepository>();
        }

        /// <summary>
        /// Add service to send email
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void AddClients (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTannerRestClients(configuration)
               .UseClient<IAPIManagerClient, APIManagerClient>("APIManagerClient");
        }

        /// <summary>
        /// Add db context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTannerDataAccess(configuration)
                .UseContext<CoreContext>(DBKEY_Core)
                .AddCommand<DeleteDocumentQuotationCommand, OperationBaseResult>()
                .AddCommand<AddClientCheckingAccountCommand, OperationResult<CurrentAccountResource>>()
                .AddCommand<InsertFileOperationCommand, OperationResult<FileResource>>()
                .UseContext<CoreFileContext>(DBKEY_CoreFile)
                .UseContext<IntelicomContext>(DBKEY_Intelicom)
                .UseContext<SegprivContext>(DBKEY_Segpriv);

        }

        /// <summary>
        /// Add custom services
        /// </summary>
        /// <param name="services"></param>
        private static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ISegprivUserService, SegprivUserService>();
        }

        private static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection config = configuration.GetSection(CorsConfiguration.SectionName);
            services.Configure<CorsConfiguration>(config);

            IConfigurationSection configSwagger = configuration.GetSection(SwaggerBasicAuth.Key);
            services.Configure<SwaggerBasicAuth>(configSwagger);

            IConfigurationSection email = configuration.GetSection(EmailConfiguration.Key);
            services.Configure<EmailConfiguration>(email);

            IConfigurationSection configIOF = configuration.GetSection(TannerIOFConfiguration.Key);
            services.Configure<TannerIOFConfiguration>(configIOF);
        }
    }
}
