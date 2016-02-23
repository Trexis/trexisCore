/*
 * Created by SharpDevelop.
 * User: hendrik.tredoux
 * Date: 4/29/2013
 * Time: 5:55 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace treXis.core
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class treXisCore
	{
		public treXisCore(){
			
		}
		
		public DAL DAL(){
			return new DAL("my02.winhost.com", "mysql_58070_trexis", "trexis", "!tr3x1s!");
		}
	}
}