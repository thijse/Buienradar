#region BuienRadarAPI - MIT - (c) 2017 Thijs Elenbaas.
/*
  DS Photosorter - tool that processes photos captured with Synology DS Photo

  Permission is hereby granted, free of charge, to any person obtaining
  a copy of this software and associated documentation files (the
  "Software"), to deal in the Software without restriction, including
  without limitation the rights to use, copy, modify, merge, publish,
  distribute, sublicense, and/or sell copies of the Software, and to
  permit persons to whom the Software is furnished to do so, subject to
  the following conditions:

  The above copyright notice and this permission notice shall be
  included in all copies or substantial portions of the Software.

  Copyright 2017 - Thijs Elenbaas
*/
#endregion

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Buienradar
{
    [XmlRoot(ElementName = "image")]
    public class Image
    {
        [XmlElement(ElementName = "titel")]
        public string Titel { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "width")]
        public string Width { get; set; }

        [XmlElement(ElementName = "height")]
        public string Height { get; set; }
    }

    [XmlRoot(ElementName = "stationnaam")]
    public class Stationnaam
    {
        [XmlAttribute(AttributeName = "regio")]
        public string Regio { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "icoonactueel")]
    public class IcoonActueel
    {
        [XmlAttribute(AttributeName = "zin")]
        public string Zin { get; set; }

        [XmlAttribute(AttributeName = "ID")]
        public string ID { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "weerstation")]
    public class Weerstation
    {
        [XmlElement(ElementName = "stationcode")]
        public string StationCode { get; set; }

        [XmlElement(ElementName = "stationnaam")]
        public Stationnaam StationNaam { get; set; }

        [XmlElement(ElementName = "lat")]
        public string Latitude { get; set; }

        [XmlElement(ElementName = "lon")]
        public string Longitude { get; set; }

        [XmlElement(ElementName = "datum")]
        public string Datum { get; set; }

        [XmlElement(ElementName = "luchtvochtigheid")]
        public string Luchtvochtigheid { get; set; }

        [XmlElement(ElementName = "temperatuurGC")]
        public string TemperatuurGrondCelcius { get; set; }

        [XmlElement(ElementName = "windsnelheidMS")]
        public string WindsnelheidMS { get; set; }

        [XmlElement(ElementName = "windsnelheidBF")]
        public string WindsnelheidBeaufort { get; set; }

        [XmlElement(ElementName = "windrichtingGR")]
        public string WindrichtingGraden { get; set; }

        [XmlElement(ElementName = "windrichting")]
        public string Windrichting { get; set; }

        [XmlElement(ElementName = "luchtdruk")]
        public string Luchtdruk { get; set; }

        [XmlElement(ElementName = "zichtmeters")]
        public string Zichtmeters { get; set; }

        [XmlElement(ElementName = "windstotenMS")]
        public string WindstotenMs { get; set; }

        [XmlElement(ElementName = "regenMMPU")]
        public string RegenMmPerUur { get; set; }

        [XmlElement(ElementName = "icoonactueel")]
        public IcoonActueel IcoonActueel { get; set; }

        [XmlElement(ElementName = "temperatuur10cm")]
        public string Temperatuur10cm { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "latGraden")]
        public string LattitudeGraden { get; set; }

        [XmlElement(ElementName = "lonGraden")]
        public string LongitudeGraden { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "weerstations")]
    public class Weerstations
    {
        [XmlElement(ElementName = "weerstation")]
        public List<Weerstation> Weerstation { get; set; }
    }

    [XmlRoot(ElementName = "buienindex")]
    public class Buienindex
    {
        [XmlElement(ElementName = "waardepercentage")]
        public string Waardepercentage { get; set; }

        [XmlElement(ElementName = "datum")]
        public string Datum { get; set; }
    }

    [XmlRoot(ElementName = "buienradar")]
    public class Buienradar
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "urlbackup")]
        public string Urlbackup { get; set; }

        [XmlElement(ElementName = "icoonactueel")]
        public IcoonActueel IcoonActueel { get; set; }

        [XmlElement(ElementName = "zonopkomst")]
        public string Zonopkomst { get; set; }

        [XmlElement(ElementName = "zononder")]
        public string Zononder { get; set; }

        [XmlElement(ElementName = "aantalonweer")]
        public string AantalOnweer { get; set; }

        [XmlElement(ElementName = "aantalhagel")]
        public string AantalHagel { get; set; }
    }

    [XmlRoot(ElementName = "actueel_weer")]
    public class ActueelWeer
    {
        [XmlElement(ElementName = "weerstations")]
        public Weerstations Weerstations { get; set; }

        [XmlElement(ElementName = "buienindex")]
        public Buienindex Buienindex { get; set; }

        [XmlElement(ElementName = "buienradar")]
        public Buienradar Buienradar { get; set; }
    }

    [XmlRoot(ElementName = "tekst_middellang")]
    public class TekstMiddellang
    {
        [XmlAttribute(AttributeName = "periode")]
        public string Periode { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "tekst_lang")]
    public class TekstLang
    {
        [XmlAttribute(AttributeName = "periode")]
        public string Periode { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "icoon")]
    public class Icoon
    {
        [XmlAttribute(AttributeName = "ID")]
        public string ID { get; set; }

        [XmlText]
        public string Text { get; set; }
    }


    public class Dagplus
    {
        [XmlElement(ElementName = "datum")]
        public string Datum { get; set; }

        [XmlElement(ElementName = "dagweek")]
        public string DagWeek { get; set; }

        [XmlElement(ElementName = "kanszon")]
        public string KansZon { get; set; }

        [XmlElement(ElementName = "kansregen")]
        public string KansRegen { get; set; }

        [XmlElement(ElementName = "minmmregen")]
        public string MinMMRegen { get; set; }

        [XmlElement(ElementName = "maxmmregen")]
        public string MaxMMRegen { get; set; }

        [XmlElement(ElementName = "mintemp")]
        public string Mintemp { get; set; }

        [XmlElement(ElementName = "mintempmax")]
        public string Mintempmax { get; set; }

        [XmlElement(ElementName = "maxtemp")]
        public string Maxtemp { get; set; }

        [XmlElement(ElementName = "maxtempmax")]
        public string Maxtempmax { get; set; }

        [XmlElement(ElementName = "windrichting")]
        public string Windrichting { get; set; }

        [XmlElement(ElementName = "windkracht")]
        public string Windkracht { get; set; }

        [XmlElement(ElementName = "icoon")]
        public Icoon Icoon { get; set; }

        [XmlElement(ElementName = "sneeuwcms")]
        public string SneeuwCms { get; set; }
    }

    [XmlRoot(ElementName = "dag-plus1")]
    public class Dagplus1 : Dagplus
    {
    }

    [XmlRoot(ElementName = "dag-plus2")]
    public class Dagplus2 : Dagplus
    {
    }


    [XmlRoot(ElementName = "dag-plus3")]
    public class Dagplus3 : Dagplus
    {
    }

    [XmlRoot(ElementName = "dag-plus4")]
    public class Dagplus4 : Dagplus
    {
    }

    [XmlRoot(ElementName = "dag-plus5")]
    public class Dagplus5 : Dagplus
    {
    }


    [XmlRoot(ElementName = "verwachting_meerdaags")]
    public class VerwachtingMeerdaags
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "urlbackup")]
        public string Urlbackup { get; set; }

        [XmlElement(ElementName = "tekst_middellang")]
        public TekstMiddellang TekstMiddellang { get; set; }

        [XmlElement(ElementName = "tekst_lang")]
        public TekstLang Tekst_lang { get; set; }

        [XmlElement(ElementName = "dag-plus1")]
        public Dagplus1 Dagplus1 { get; set; }

        [XmlElement(ElementName = "dag-plus2")]
        public Dagplus2 Dagplus2 { get; set; }

        [XmlElement(ElementName = "dag-plus3")]
        public Dagplus3 Dagplus3 { get; set; }

        [XmlElement(ElementName = "dag-plus4")]
        public Dagplus4 Dagplus4 { get; set; }

        [XmlElement(ElementName = "dag-plus5")]
        public Dagplus5 Dagplus5 { get; set; }
    }

    [XmlRoot(ElementName = "verwachting_vandaag")]
    public class VerwachtingVandaag
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "urlbackup")]
        public string Urlbackup { get; set; }

        [XmlElement(ElementName = "titel")]
        public string Titel { get; set; }

        [XmlElement(ElementName = "tijdweerbericht")]
        public string Tijdweerbericht { get; set; }

        [XmlElement(ElementName = "samenvatting")]
        public string Samenvatting { get; set; }

        [XmlElement(ElementName = "tekst")]
        public string Tekst { get; set; }

        [XmlElement(ElementName = "formattedtekst")]
        public string Formattedtekst { get; set; }
    }

    [XmlRoot(ElementName = "weergegevens")]
    public class Weergegevens
    {
        [XmlElement(ElementName = "titel")]
        public string Titel { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "omschrijving")]
        public string Omschrijving { get; set; }

        [XmlElement(ElementName = "language")]
        public string Language { get; set; }

        [XmlElement(ElementName = "copyright")]
        public string Copyright { get; set; }

        [XmlElement(ElementName = "gebruik")]
        public string Gebruik { get; set; }

        [XmlElement(ElementName = "image")]
        public Image Image { get; set; }

        [XmlElement(ElementName = "actueel_weer")]
        public ActueelWeer ActueelWeer { get; set; }

        [XmlElement(ElementName = "verwachting_meerdaags")]
        public VerwachtingMeerdaags VerwachtingMeerdaags { get; set; }

        [XmlElement(ElementName = "verwachting_vandaag")]
        public VerwachtingVandaag VerwachtingVandaag { get; set; }
    }

    [XmlRoot(ElementName = "buienradarnl")]
    public class WeersInformatieBuienRadar
    {
        [XmlElement(ElementName = "weergegevens")]
        public Weergegevens Weergegevens { get; set; }
    }
}