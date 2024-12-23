import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SearchService } from '../../../../core/services/search.service';
import { SearchEngine } from '../../../shared/enums/search-engine.enum';
import { getSearchEngineOptions } from '../utils/search-engine.utils';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent {
  searchForm: FormGroup;
  searchEngines = getSearchEngineOptions();
  result: any;

  constructor(private fb: FormBuilder, private searchService: SearchService) {
    this.searchForm = this.fb.group({
      keywords: ['', Validators.required],
      url: ['', [Validators.required, Validators.pattern('https?://.+')]],
      searchEngine: [SearchEngine.Google, Validators.required],
    });
  }

  onSubmit(): void {
    if (this.searchForm.valid) {
      const { keywords, url, searchEngine } = this.searchForm.value;
      this.searchService.search(searchEngine, keywords, url).subscribe({
        next: (response) => {
          this.result = response;
        },
        error: (error) => {
          console.error('Search failed:', error);
        },
      });
    }
  }
}
