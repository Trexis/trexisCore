/*
 * Created by SharpDevelop.
 * User: hendrik.tredoux
 * Date: 4/29/2013
 * Time: 5:56 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

using treXis.rateit;

namespace treXis.rest
{
	[ServiceContract]
	public interface IService
	{
	   [OperationContract]
	   [WebGet(UriTemplate = "rateit/{ratingtype}")]
	   string GetRating(string ratingtype);

	   [OperationContract]
	   [WebGet(UriTemplate = "operation/{name}")]
	   string MyOperation(string name);
	}
	
	public class Service : IService
	{
		public string GetRating(string RatingType){
			treXisRateIt rateit = new treXisRateIt();
			Dictionary<string, string> ratings = rateit.getRatings("hendrik", RatingType, DateTime.Now, DateTime.Now);
			return RatingType;
		}
		
	   public string MyOperation(string name)
	   {
		  // implement the operation
		  return string.Format("Operation name: {0}", name);
	   }
	} 
}
