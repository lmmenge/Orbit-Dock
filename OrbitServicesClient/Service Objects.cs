namespace Orbit.OrbitServices.OrbitServicesClient
{
	public struct ExtendedError
	{
		public string Name;
		public string Email;
		public string PresentInVersion;
		public string Date;
		public string Description;

		public override bool Equals(object obj)
		{
			ExtendedError other=(ExtendedError)obj;

			if(this.Name==other.Name
				&& this.Email==other.Email
				&& this.PresentInVersion==other.PresentInVersion
				&& this.Date==other.Date
				&& this.Description==other.Description)
				return true;
			else
				return false;
		}

		public override int GetHashCode()
		{
			return (Name+Email+PresentInVersion+Date+Description).GetHashCode();
		}

        
		public static bool operator ==(ExtendedError op1, ExtendedError op2)
		{
			return op1.Equals(op2);
		}

		public static bool operator !=(ExtendedError op1, ExtendedError op2)
		{
			return !op1.Equals(op2);
		}
	}
}