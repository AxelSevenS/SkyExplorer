import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { environment } from './environments/environment';
import { SkyExplorerRoutingModule } from './app/skyexplorer-routing.module';


if (environment.production) {
	enableProdMode();
}

platformBrowserDynamic().bootstrapModule(SkyExplorerRoutingModule)
	.catch(err => console.log(err));