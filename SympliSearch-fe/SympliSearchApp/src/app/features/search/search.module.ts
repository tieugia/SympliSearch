import { NgModule } from '@angular/core';
import { SearchComponent } from './components/search.component';
import { SharedModule } from '../../shared/shared.module';
import { SearchRoutingModule } from './search-routing.module';
import { SearchEngineNamePipe } from '../../base/pipes/pipe';
@NgModule({
  declarations: [SearchComponent, SearchEngineNamePipe],
  imports: [SharedModule, SearchRoutingModule],
})
export class SearchModule {}
