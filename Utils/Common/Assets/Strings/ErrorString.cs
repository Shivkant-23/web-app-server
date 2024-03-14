using System;
namespace web_server.Utils.Common.Assets.Strings
{
	public class ErrorString
	{
		public ErrorString()
		{
		}

		public String NoInternetError() {
			return "Internet service is not working, please check internet connection!";
		}

        public String FileNotFoundError()
        {
            return "File is not found, please try again!";
        }

        public bool TextMatch(String input)
        {
            return input == "input";
        }
    }
}

