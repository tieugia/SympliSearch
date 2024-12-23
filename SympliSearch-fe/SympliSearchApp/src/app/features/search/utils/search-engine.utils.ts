import { SearchEngine } from "../../../shared/enums/search-engine.enum";

export function getSearchEngineOptions(): { label: string; value: number }[] {
  return Object.keys(SearchEngine)
    .filter((key) => isNaN(Number(key)))
    .map((key) => ({
      label: key,
      value: SearchEngine[key as keyof typeof SearchEngine],
    }));
}
