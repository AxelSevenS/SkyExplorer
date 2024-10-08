import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { environment } from './environments/environment';
import { SkyExplorerModule } from './app/skyexplorer.module';


if (environment.production) {
	enableProdMode();
}

platformBrowserDynamic().bootstrapModule(SkyExplorerModule)
	.catch(err => console.log(err));