// <copyright file="ElevatorController.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// The elevator controller class. Implements the <see cref="ControllerBase" />.
	/// </summary>
	/// <seealso cref="ControllerBase" />
	[ApiController]
	[Route("[controller]")]
	public class ElevatorController : ControllerBase
	{
		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<ElevatorController> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="ElevatorController" /> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		public ElevatorController(ILogger<ElevatorController> logger) => _logger = logger;

		///// <summary>
		///// Gets this instance.
		///// </summary>
		///// <returns>IEnumerable&lt;WeatherForecast&gt;.</returns>
		//[HttpGet]
		//public IEnumerable<WeatherForecast> Get()
		//{
		//	var rng = new Random();
		//	return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		//	{
		//		Date = DateTime.Now.AddDays(index),
		//		TemperatureC = rng.Next(-20, 55),
		//		Summary = Summaries[rng.Next(Summaries.Length)]
		//	})
		//	.ToArray();
		//}
	}
}
