Currency to Text Converter

Quick start
-	Open the solution CurrencyToTextConverter.slnx in Visual Studio (recommended version: 2026).
-	To build the application: Build → Build Solution.
-	To run the application: Set up multiple startup projects (GUI and Server), then press Start.
-	The port used by the Server and the GUI can be changed from appsettings.json files of both projects.
-	To run the tests: Right click on CurrencyToTextConverter.Tests project and press Run Tests.

Key design notes
-	The solution is split into three projects: GUI, Server, and Tests.
-	Convertion logic is implemented in the Server project.
-	Amount parsing and validation are centralized in AmountValidator of the Server project.
-	Amount can contain a comma as decimal separator (e.g. "2,15").
-	Amount can contain spaces as thousands separators (e.g. "40 000").
-	Valid amounts are between 0 and 999 999 999,99 (integer part up to 999 999 999 and fraction up to 99).
-	The validator returns a long integer part and an int fraction.
-	Currency converters consume (integer, fraction) and return language specific text.
-	Currency converters generate the text representations of the numbers by converting the numbers recursively from 
	largest to smallest (millions, thousands, hundreds, tens, teens, units).
-	Two currency converters are provided: English and German.
-	One currency descriptor is provided: dollar.
-	The architecture supports adding additional language-specific currency converters as well as additional currency 
	descriptors with minimal changes.

Defaults
-	Default language is English and default currency is dollar.
-	Default port is 32500.




