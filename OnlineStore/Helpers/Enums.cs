using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnlineStore.Helpers
{
    public class Enums
    {
        public enum Gender
        {
            men,
            women
        }

        public enum Order_Status
        {
            Pending,
            Processing,
            Rejected,
            Delivered,
            Cancelled,
            Accepted,
            [EnumMember(Value = "Out for delivery")]
            Outfordelivery
        }

        public enum Styles
        {
            Casual,
            Athletic,
            Business,
            Classic
        }

        //[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum Categories
        {
            Top,
            Bottom,
            Dresses,
            Shirts,
            //[EnumMember(Value = "T-Shirts")]
            T_Shirts,
            Polo,
            Pants,
            Joggers,
            Jeans,
            Jackets,
            Sweaters
        }

        public enum Colors
        {
            Gray,
            White,
            Black,
            Red,
            Blue,
            
        }

        public enum Seasons
        {
            Summer,
            Winter,
            Autumn
            
        }

        public enum Materials
        {
            Cotton,
            Jeans,
            Polyster,
            Silk
        }

        public enum Sizes
        {
            S,
            M,
            L,
            XL
        }

        public enum Countries
        {
            Germany,
            France,
            Egypt,
            KSA,
            Canada
        }
        public enum Cities
        {
            Berlin,
            Frankfurt,
            Paris,
            Cairo,
            Alexandria,
            Giza,
            Jedaah,
            Montreal
        }
        public enum Brands
        {
            Gucci,
            Nike,
            Guess,
            Addidas,
            Zara,
            Armani
        }

        public enum PaymentTypes
        {
            PayPal,
            Visa,
            Cash
        }

        public enum ShippingMethods
        {
            Fedex,
            Expedited,
            [EnumMember(Value = "Same day delivery")]
            SameDay
        }
    }
}
