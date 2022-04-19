﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Module CountryCodes

    Public ReadOnly s_completeCountryCodeList As New Dictionary(Of String, String) From {
        {"United States", "US"},
        {"Andorra", "AD"},
        {"United Arab Emirates", "AE"},
        {"Afghanistan", "AF"},
        {"Antiguaand Barbuda", "AG"},
        {"Anguilla", "AI"},
        {"Albania", "AL"},
        {"Armenia", "AM"},
        {"Netherland Antilles", "AN"},
        {"Angola", "AO"},
        {"Antarctica", "AQ"},
        {"Argentina", "AR"},
        {"American Samoa", "AS"},
        {"Austria", "AT"},
        {"Australia", "AU"},
        {"Aruba", "AW"},
        {"Azerbaidjan", "AZ"},
        {"Bosnia-Herzegovina", "BA"},
        {"Barbados", "BB"},
        {"Banglades", "BD"},
        {"Belgium", "BE"},
        {"Burkina Faso", "BF"},
        {"Bulgaria", "BG"},
        {"Bahrain", "BH"},
        {"Burundi", "BI"},
        {"Benin", "BJ"},
        {"Bermuda", "BM"},
        {"Brunei Darussalam", "BN"},
        {"Bolivia", "BO"},
        {"Brazil", "BR"},
        {"Bahamas", "BS"},
        {"Buthan", "BT"},
        {"Bouvet Island", "BV"},
        {"Botswana", "BW"},
        {"Belarus", "BY"},
        {"Belize", "BZ"},
        {"Canada", "CA"},
        {"Cocos (Keeling) Isl.", "CC"},
        {"Central African Rep.", "CF"},
        {"Congo", "CG"},
        {"Switzerland", "CH"},
        {"IvoryCoast", "CI"},
        {"Cook Islands", "CK"},
        {"Chile", "CL"},
        {"Cameroon", "CM"},
        {"China", "CN"},
        {"Colombia", "CO"},
        {"Costa Rica", "CR"},
        {"Czechoslovakia", "CS"},
        {"Cuba", "CU"},
        {"Cape Verde", "CV"},
        {"Christmas Island", "CX"},
        {"Cyprus", "CY"},
        {"Czech Republic", "CZ"},
        {"Germany", "DE"},
        {"Djibouti", "DJ"},
        {"Denmark", "DK"},
        {"Dominica", "DM"},
        {"Dominican Republic", "DO"},
        {"Algeria", "DZ"},
        {"Ecuador", "EC"},
        {"Estonia", "EE"},
        {"Egypt", "EG"},
        {"Western Sahara", "EH"},
        {"Spain", "ES"},
        {"Ethiopia", "ET"},
        {"Finland", "FI"},
        {"Fiji", "FJ"},
        {"Falkland Isl. (Malvinas)", "FK"},
        {"Micronesia", "FM"},
        {"Faroe Islands", "FO"},
        {"France", "FR"},
        {"France (EuropeanTer.)", "FX"},
        {"Gabon", "GA"},
        {"Great Britain (UK)", "GB"},
        {"Grenada", "GD"},
        {"Georgia", "GE"},
        {"Guyana (Fr.)", "GF"},
        {"Ghana", "GH"},
        {"Gibraltar", "GI"},
        {"Greenland", "GL"},
        {"Gambia", "GM"},
        {"Guinea", "GN"},
        {"Guadeloupe (Fr.)", "GP"},
        {"Equatorial Guinea", "GQ"},
        {"Greece", "GR"},
        {"Guatemala", "GT"},
        {"Guam (US)", "US"},
        {"Guinea Bissau", "GW"},
        {"Guyana", "GY"},
        {"Hong Kong", "HK"},
        {"Heard & McDonald Isl.", "HM"},
        {"Honduras", "HN"},
        {"Croatia", "HR"},
        {"Haiti", "HT"},
        {"Hungary", "HU"},
        {"Indonesia", "ID"},
        {"Ireland", "IE"},
        {"Israel", "IL"},
        {"India", "IN"},
        {"British Indian O. Terr.", "IO"},
        {"Iraq", "IQ"},
        {"Iran", "IR"},
        {"Iceland", "IS"},
        {"Italy", "IT"},
        {"Jamaica", "JM"},
        {"Jordan", "JO"},
        {"Japan", "JP"},
        {"Kenya", "KE"},
        {"Kirgistan", "KG"},
        {"Cambodia", "KH"},
        {"Kiribati", "KI"},
        {"Comoros", "KM"},
        {"St. Kitts Nevis Anguilla", "KN"},
        {"Korea (North)", "KP"},
        {"Korea (South)", "KR"},
        {"Kuwait", "KW"},
        {"Cayman Islands", "KY"},
        {"Kazachstan", "KZ"},
        {"Laos", "LA"},
        {"Lebanon", "LB"},
        {"Saint Lucia", "LC"},
        {"Liechtenstein", "LI"},
        {"Sri Lanka", "LK"},
        {"Liberia", "LR"},
        {"Lesotho", "LS"},
        {"Lithuania", "LT"},
        {"Luxembourg", "LU"},
        {"Latvia", "LV"},
        {"Libya", "LY"},
        {"Morocco", "MA"},
        {"Monaco", "MC"},
        {"Moldavia", "MD"},
        {"Madagascar", "MG"},
        {"Marshall Islands", "MH"},
        {"Mali", "ML"},
        {"Myanmar", "MM"},
        {"Mongolia", "MN"},
        {"Macau", "MO"},
        {"Northern Mariana Isl.", "MP"},
        {"Martinique (Fr.)", "MQ"},
        {"Mauritania", "MR"},
        {"Montserrat", "MS"},
        {"Malta", "MT"},
        {"Mauritius", "MU"},
        {"Maldives", "MV"},
        {"Malawi", "MW"},
        {"Mexico", "MX"},
        {"Malaysia", "MY"},
        {"Mozambique", "MZ"},
        {"Namibia", "NA"},
        {"New Caledonia (Fr.)", "NC"},
        {"Niger", "NE"},
        {"Norfolk Island", "NF"},
        {"Nigeria", "NG"},
        {"Nicaragua", "NI"},
        {"Netherlands", "NL"},
        {"Norway", "NO"},
        {"Nepal", "NP"},
        {"Nauru", "NR"},
        {"Neutral Zone", "NT"},
        {"Niue", "NU"},
        {"New Zealand", "NZ"},
        {"Oman", "OM"},
        {"Panama", "PA"},
        {"Peru", "PE"},
        {"Polynesia (Fr.)", "PF"},
        {"PapuaNew", "PG"},
        {"Philippines", "PH"},
        {"Pakistan", "PK"},
        {"Poland", "PL"},
        {"St. Pierre & Miquelon", "PM"},
        {"Pitcairn", "PN"},
        {"Puerto Rico (US)", "US"},
        {"Portugal", "PT"},
        {"Palau", "PW"},
        {"Paraguay", "PY"},
        {"Qatar", "QA"},
        {"Reunion (Fr.)", "RE"},
        {"Romania", "RO"},
        {"Russian Federation", "RU"},
        {"Rwanda", "RW"},
        {"Saudi Arabia", "SA"},
        {"Solomon Islands", "SB"},
        {"Seychelles", "SC"},
        {"Sudan", "SD"},
        {"Sweden", "SE"},
        {"Singapore", "SG"},
        {"St.Helena", "SH"},
        {"Slovenia", "SI"},
        {"Svalbard & Jan Mayen Is", "SJ"},
        {"Slovak Republic", "SK"},
        {"Sierra Leone", "SL"},
        {"San Marino", "SM"},
        {"Senegal", "SN"},
        {"Somalia", "SO"},
        {"Suriname", "SR"},
        {"St. Tomeand Principe", "ST"},
        {"Soviet Union", "SU"},
        {"El Salvador", "SV"},
        {"Syria", "SY"},
        {"Swaziland", "SZ"},
        {"Turks & Caicos Islands", "TC"},
        {"Chad", "TD"},
        {"French Southern Terr.", "TF"},
        {"Togo", "TG"},
        {"Thailand", "TH"},
        {"Tadjikistan", "TJ"},
        {"Tokelau", "TK"},
        {"Turkmenistan", "TM"},
        {"Tunisia", "TN"},
        {"Tonga", "TO"},
        {"East Timor", "TP"},
        {"Turkey", "TR"},
        {"Trinidad & Tobago", "TT"},
        {"Tuvalu", "TV"},
        {"Taiwan", "TW"},
        {"Tanzania", "TZ"},
        {"Ukraine", "UA"},
        {"Uganda", "UG"},
        {"United Kingdom", "UK"},
        {"US Minor outlying Isl.", "US"},
        {"Uruguay", "UY"},
        {"Uzbekistan", "UZ"},
        {"Vatican City State", "VA"},
        {"St.Vincen & Grenadines", "VC"},
        {"Venezuela", "VE"},
        {"Virgin Islands (British)", "VG"},
        {"Virgin Islands (US)", "VI"},
        {"Vietnam", "VN"},
        {"Vanuatu", "VU"},
        {"Wallis & Futuna Islands", "WF"},
        {"Samoa", "WS"},
        {"Yemen", "YE"},
        {"Yugoslavia", "YU"},
        {"South Africa", "ZA"},
        {"Zambia", "ZM"},
        {"Zaire", "ZR"},
        {"Zimbabwe", "ZW	"}
    }

    Public ReadOnly s_countryCodeList As New Dictionary(Of String, String) From {
            {"United States", "US"},
        {"Australia", "AU"},
        {"Europe", "EU"}
        }

End Module
