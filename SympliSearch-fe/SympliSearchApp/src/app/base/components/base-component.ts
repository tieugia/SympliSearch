import { Directive } from "@angular/core";
import { Subject } from "rxjs";

@Directive()
export abstract class BaseComponent{
  private _destroy$: Subject<any> | undefined;

  get destroy$(): Subject<any>{
    if(!this._destroy$){
      this._destroy$ = new Subject();
    }
    return this._destroy$;
  }

  ngOnDestroy(): void{
    if(this._destroy$){
      this._destroy$.next(true);
      this._destroy$.complete();
    }
  }
}
