// <copyright file="Program.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using WebApp;

Host
	.CreateDefaultBuilder(args)
	.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
	.Build()
	.Run();
