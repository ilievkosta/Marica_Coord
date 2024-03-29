﻿using BojkoSoft.Transformations;
using BojkoSoft.Transformations.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace Marica_Coord
{
    public class Cord
    {
        public string nameC { get; set; }

        public double xC { get; set; }

        public double yC { get; set; }

        public double zC { get; set; } 

        public Cord()
        {

        }
        public Cord(string name, double x, double y, double z)
        {
            nameC = name;
            xC = x;
            yC = y;
            zC = z;
        }

        public static string SerializeToXml(object value)
        {
            StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
            XmlSerializer serializer = new XmlSerializer(value.GetType());
            serializer.Serialize(writer, value);
            return writer.ToString();
        }

        public static List<Cord> DeserializeObject(string data)
        {

          

            var xml = data;
            var serializer = new XmlSerializer(typeof(List<Cord>));
            List<Cord> list = new List<Cord>();
            using (var reader = new StringReader(xml))
            {
      
                list = (List < Cord >)serializer.Deserialize(reader);
            }

                
            
            return list;
        }
    

    public static void Mto1970(List<Cord> CoordinatesList)
        {
            
            for(int i = 0; i < CoordinatesList.Count; i++)
            {
                CoordinatesList[i].xC += 4580000;
                
                CoordinatesList[i].yC += 9430000;
              
            }


           
        }
        public static IList<Cord> B70toM(List<Cord> CoordinatesList)
        {

            for (int i = 0; i < CoordinatesList.Count; i++)
            {
                CoordinatesList[i].xC -= 4580000;
                CoordinatesList[i].yC -= 9430000;

            }

            return CoordinatesList;
        }

        public static IList<Cord> Inverce(List<Cord> CoordinatesList)
        {
            Double x, y;
           
            for (int i = 0; i < CoordinatesList.Count; i++)
            {
                x = CoordinatesList[i].yC;
                y = CoordinatesList[i].xC;
                CoordinatesList[i].xC = x;
                CoordinatesList[i].yC = y;

            }
            return CoordinatesList;
        }
            public static IList<Cord> C70toUTM(List<Cord> CoordinatesList)
        {
            for (int i = 0; i < CoordinatesList.Count; i++)
            {

                Transformations tr = new Transformations();

               GeoPoint input = new GeoPoint(CoordinatesList[i].xC, CoordinatesList[i].yC);
               GeoPoint result = tr.TransformBGSCoordinates(input, enumProjection.BGS_1970_K5, enumProjection.BGS_2005_KK);



                CoordinatesList[i].xC = result.X;
                CoordinatesList[i].yC = result.Y;


            }

            return CoordinatesList;
        }
        public static string printCord(List<Cord> Cordinates, string delimiter, string newLine)
        {

            string output = "";
            for (int i = 0; i < Cordinates.Count; i++)
            {
                output += Cordinates[i].nameC + delimiter + Cordinates[i].xC + delimiter + Cordinates[i].yC + delimiter + Cordinates[i].zC + newLine;
            }
            return output;
        }



    }

}