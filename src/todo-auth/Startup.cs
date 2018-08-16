// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Data.SqlClient;
using System.Reflection;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Auth.Data.Users;
using Todo.Auth.Models;

namespace Todo.Auth
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseOptions>(Configuration.GetSection("database"));
            DatabaseOptions dbOptions = services.BuildServiceProvider().GetService<DatabaseOptions>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(GetConnectionString(dbOptions)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            string connectionString = GetConnectionString(dbOptions);
            string migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            IIdentityServerBuilder builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.IssuerUri = "http://auth:5000";
            })
                .AddAspNetIdentity<ApplicationUser>()
                //this adds the config data from DB (clients, resources, CORS)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    // options.TokenCleanupInterval = 15; // interval in seconds. 15 seconds useful for debugging
                });

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "141503743850-s534b38nqbi1aor5nbrc984ctpdcktnv.apps.googleusercontent.com";
                    options.ClientSecret = "UG0I9uog8UVcjUQEYfumHbVO";
                });

            services.UseAdminUI();
        }

        private string GetConnectionString(DatabaseOptions databaseOptions)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = databaseOptions.Server;
            builder.UserID = databaseOptions.UserId;
            builder.Password = databaseOptions.Password;

            if (!string.IsNullOrEmpty(databaseOptions.Name))
            {
                builder.InitialCatalog = databaseOptions.Name;
            }

            return builder.ToString();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
            app.UseAdminUI();
        }
    }

    public class DatabaseOptions
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Name { get; set; }
    }
}
