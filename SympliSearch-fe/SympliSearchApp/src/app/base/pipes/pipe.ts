import { Pipe, PipeTransform } from '@angular/core';
import { SearchEngine } from '../../shared/enums/search-engine.enum';

@Pipe({
  name: 'searchEngineName'
})
export class SearchEngineNamePipe implements PipeTransform {
  transform(value: SearchEngine): string {
    return SearchEngine[value];
  }
}