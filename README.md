# DNNSurvey
DNN Survey module MVC based

##Features

##Installation notes
To enable MVC modules in DNN 7.5.x please ensure the following module is registered in the <system.webServer><modules> element:

	<system.webServer>
		<modules>
			<add name="MVCModules" type="DotNetNuke.Web.Mvc.MvcHttpModule, DotNetNuke.Web.Mvc" preCondition="managedHandler" />
		</modules>
	</system.webServer>
