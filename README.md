# i18n-cs
This is a collection of Visual Studio 2015 Applications to demonstrate internationalization in C# Code, WinForms, WPF and WIX installers.
The default language is English and translations to Japanese are added. This approach is recommended so systems other than Japanese or English will see an English interface.

# C#

## C# Application's default culture
To define the application's default culture (In this case English), the _NeutralResourcesLanguageAttribute_ AssemblyInfo must be set:

**Hint:** Always use _English_ and not a specific English locale such as _English (United States)_

![AssemblyInfo Default Culture](tutorial_img/AssemblyInfo_NeutralResourcesLanguageAttribute.png)

## C# WinForms
This Tutorial is based on Microsofts MSDN Tutorial [Walkthrough: Localizing Windows Forms](https://msdn.microsoft.com/en-us/library/y99d1cd3(v=vs.100).aspx)

Following resources are used for internationalzation:

* Project resources (non-form-based, dialog-boxes, error-messages)
* Form resources (Auto-generated)

**Hint:** For Forms-Property: always use either project OR form resources, not mixed

### Localizable Forms (Form resources)

1. Set project as localizable

	![Set localizable](tutorial_img/1_enablei18n.png)

2. Open Form to translate

	![Form to translate](tutorial_img/1_formDefaultLanguage.png)

3. Change Property Language of Form to Japanese

	![Form in Japanese](tutorial_img/1_formJapanese.png)

4. Set Text of desired Element (e.g. Button) to translated new Text.

	![Form in Japanese with Japanese text](tutorial_img/1_formJapaneseTextEdited.png)

### Localizable Error-message and dialog-boxes (Project resources)

This part is also applicable to simple Console-applications

1. Add new Resource-file to the Project

	**Hint:** This file is the fallback for the current default language (English), so the text in this file should be English.

	![New Project resources](tutorial_img/2_projectResourceName.png)
	
2. Enter a new string with a default English text.

	![Japanese Text](tutorial_img/2_projectResEn.png)

3. Repeat step 1 and 2 with a new file named _WinFormStrings.ja.resx_

	![Japanese Text](tutorial_img/2_projectResJp.png)

4. To access the manually added resources (e.g. on a button-click) use the following code:

		using System.Resources;
		
		...
		
		// Access resource by Getter (Recommended as by http://stackoverflow.com/a/14503044/2003325)
		MessageBox.Show(WinFormStrings.errorInsuffMemory);

**Hint:** To force the program to start in a specific locale, uncomment one of these lines in _Program.cs_ 

	//Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP"); //Japanese
    //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB"); //English (Default of this project)
	
**Hint:** Instead of creating a new default resources-file the existing default Resources.resx file under Properties may be used. (See [this stackoverflow answer](http://stackoverflow.com/a/1129152/2003325)).
For this, only a new Resources file for the additional language Japanese must be added: _Resources.ja.resx_.
However, this approach results that ALL strings will be in one file and can't be distinguished. So separated Resources-files are strongly recommended!
	
## C# Code
See WinForms -> Localizable Error-message and dialog-boxes (Project resources)

## C# Fallback to Satellite Assembly
Usually the application fallback is the main assembly if the requested UI Culture cannot be found. 
However, if another culture should be used as a fallback, you may do so by defining a satellite assembly for fallback:

See [Packaging and Deploying Resources in Desktop Apps](https://msdn.microsoft.com/en-us/library/sb6a8618(v=vs.110).aspx) -> Ultimate Fallback to Satellite Assembly

Set in Code or in AssemblyInfo.cs

	[assembly:NeutralResourcesLanguage("en", UltimateResourceFallbackLocation.Satellite)]
	
There are various combinations of default language and satellite languages (The specfic VS-Project in bold):

* **CodeDefaultEnglish** - Default language is English, Satellite Japanese -> Fallback will be English (e.g. German user) **Recommended**
* **CodeDefaultJapanese** - Default language is Japanese, Satellite English -> Fallback will be Japanese (e.g. German user)
* **CodeJapaneseDefaultSatelliteEn** - Default language is Japanese, Satellite English AND Japanese -> Ultimate Fallback on English -> Fallback will be English

## C# WPF
This Tutorial is NOT based on Microsofts MSDN Tutorial [WPF Globalization and Localization Overview](https://msdn.microsoft.com/en-us/library/ms788718(v=vs.110).aspx)

Instead we will use the free WPFLocalizationExtension (https://github.com/SeriousM/WPFLocalizationExtension) under the [Ms-PL license](https://tldrlegal.com/license/microsoft-public-license-(ms-pl)) in combinatino with Resources .resx files.

Follow the very simple Tutorial [WPF: Localization using Resources and Localization Extension](http://www.broculos.net/2014/04/wpf-localization-using-resources-and.html#.WBlSQvqLSUk) and mind the hints below:

* You can install the WPFLocalizationExtension using NuGet:

		Install-Package WpfLocalizeExtension

* For created Resource .resx files, set the Access Modifier to _Public_

* Specify the current language to the System langauge in starting your application:

		LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
		
		//Set Extension Culture to System culture:
		LocalizeDictionary.Instance.Culture = new CultureInfo(CultureInfo.CurrentUICulture.Name);
		
* How to prepare your XAML, you may see here [Usage - Preparing the XAML code](https://wpflocalizeextension.codeplex.com/wikipage?title=Preparing%20the%20XAML%20code&referringTitle=Documentation)

		<Window xmlns:lex="http://wpflocalizeextension.codeplex.com"
			lex:LocalizeDictionary.DesignCulture="en"
			lex:ResxLocalizationProvider.DefaultAssembly="WPF"
			lex:ResxLocalizationProvider.DefaultDictionary="LocResources">
			<!-- Some controls -->
		</Window>
		
* How to access the resoruces in the XAML you may find here: [Usage - Keys](https://wpflocalizeextension.codeplex.com/wikipage?title=Keys&referringTitle=Documentation)

* If you would like to access Resources from different Assemblies in the XAML, look here: [Usage - Multiple assemblies and dictionaries](https://wpflocalizeextension.codeplex.com/wikipage?title=Multiple%20assemblies%20and%20dictionaries)
	
* Look at the [WPFLocalizationExtension Wiki](https://wpflocalizeextension.codeplex.com/documentation) for further questions

### Additional helpful features

* SizeToContent - Make window size automatic depending on content

		<Window SizeToContent="WidthAndHeight">

* SharedSizeGroup - The elements have the same size from the biggest element

		<Grid.ColumnDefinitions>
		  <ColumnDefinition x:Uid="ColumnDefinition_1" />
		  <ColumnDefinition x:Uid="ColumnDefinition_2" />
		  <ColumnDefinition x:Uid="ColumnDefinition_3" **SharedSizeGroup="Buttons"** />
		  <ColumnDefinition x:Uid="ColumnDefinition_4" **SharedSizeGroup="Buttons"** />
		  <ColumnDefinition x:Uid="ColumnDefinition_5" **SharedSizeGroup="Buttons"** />
		</Grid.ColumnDefinitions>


	
## C# Translate resource files
Zeta Resource Editor (https://www.zeta-resource-editor.com/index.html)

## C# Tree-View of resource-files with language
Howto get a treeview of culture-specific resources

Unload the project
Edit the corresponding csproj file
Locate the tags of the resources and rewrite them using the DependentUpon syntax:

	<EmbeddedResource Include="Strings.de.resx">
	  <SubType>Designer</SubType>
	  <DependentUpon>Strings.resx</DependentUpon>
	</EmbeddedResource>
	<EmbeddedResource Include="Strings.resx">
	  <Generator>ResXFileCodeGenerator</Generator>
	  <LastGenOutput>Strings.Designer.cs</LastGenOutput>
	</EmbeddedResource>
	
# Visual C++
	
# Python
