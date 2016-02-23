/*
 * Created by SharpDevelop.
 * User: hendrik.tredoux
 * Date: 4/29/2013
 * Time: 6:09 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using treXis.core;

namespace treXis.rateit
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class treXisRateIt
	{
	

		public Dictionary<string, string> getRatings(string UserId, string RatingType, DateTime StartDate, DateTime EndDate){
			try{
				
				treXisCore core = new treXisCore();
				DAL dal = core.DAL();
				
				Dictionary<string, string> input = dal.ExecuteReader("select * from ratings");
				
				return input;
				//return input.ToDictionary(item => item.Key, item => (int)item.Value);
			} catch(Exception ex){
				throw new Exception("Unable to get ratings", ex);
			}
		}
		
	}
	
}