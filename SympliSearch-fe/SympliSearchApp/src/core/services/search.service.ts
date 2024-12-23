import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SearchEngine } from '../../app/shared/enums/search-engine.enum';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  constructor(private http: HttpClient) {}

  search(engine: SearchEngine, keywords: string, url: string): Observable<any> {
    const endpoint = `/api/search/${SearchEngine[engine]}`;

    return this.http.get(endpoint, {
      params: { keywords, url },
    });
  }
}
