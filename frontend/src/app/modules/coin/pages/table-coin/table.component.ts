import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { RefreshCoinComponent } from '../refresh-coin/refresh-coin.component';
import { AddCoinComponent } from '../add-coin/add-coin.component';
import { catchError, mergeMap, of, tap, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { DeleteCoinComponent } from '../delete-coin/delete-coin.component';
import { DynamicDirective, ICoin } from '../../../../../services/types';
import { ErrorService } from '../../../../../services/error';
import { URLService } from '../../../../../services/url';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
})

export class TableCoinComponent implements OnInit {
  @ViewChild(DynamicDirective) dynamic: DynamicDirective;
  private resultCoins: ICoin[] = [];
  private increaseDecrease = 1;
  private valueCriteria = "";
  private valuesOnKey = "";
  public coins: ICoin[] = [];

  constructor(private urlService: URLService, private errorService: ErrorService) {}

  ngOnInit() {
    this.outputCoins();
  }

  cleekAdd(): void {
    const component = this.dynamic.viewContainerRef.createComponent(AddCoinComponent);
    component.instance.close.subscribe(async ()=>{
      this.dynamic.viewContainerRef.clear();
      if(component.instance.coinName){
        await this.addCoins(component.instance.coinName, new Date(component.instance.dateTime).getTime());
      }
    });
  }

  cleekRefresh(coin: ICoin): void {
    const component = this.dynamic.viewContainerRef.createComponent(RefreshCoinComponent);
    component.instance.name = coin.name;
    component.instance.close.subscribe(()=>{
    this.dynamic.viewContainerRef.clear();
      if(component.instance.send){        
        this.refreshCoins(coin.id);
      }
    });    
  }
 
  cleekDelete(coin: ICoin): void {
    const component = this.dynamic.viewContainerRef.createComponent(DeleteCoinComponent);
    component.instance.name = coin.name;
    component.instance.close.subscribe(()=>{
    this.dynamic.viewContainerRef.clear();
      if(component.instance.send){
        this.deleteCoins(coin.id);
      }
    }); 
  }

  onKey(event: any) { 
    this.valuesOnKey = event.target.value.toLowerCase();
    this.coins = [];
    this.resultCoins.forEach(coin => {
      for (var code in coin) {
        if(code != "id" && coin[code].toString().toLowerCase().indexOf(this.valuesOnKey) > -1 && code != "urlIcon"){
          this.coins.push(coin);
          break;
        }
      }
      // if(coin.name.toLowerCase().indexOf(this.valuesOnKey) > -1){
      //   this.coins.push(coin);
      // }
    });
  }

  sortBy(criteria: string): void {
    if(this.valueCriteria == criteria){
      this.increaseDecrease *= -1;
    }
    else{
      this.valueCriteria = criteria;
      this.increaseDecrease = 1;
    }

    this.coins.sort((a, b) => {
      if (a[criteria] > b[criteria]) {
        return 1 * this.increaseDecrease;
      }
      if (a[criteria] < b[criteria]) {
        return -1 * this.increaseDecrease;
      }
      return 0;
    });
  }
  
  outputCoins(){
    return this.urlService.getAllCoins()
    .subscribe({    
      next: (result : ICoin[]) =>
      { 
        this.coins = result;
        this.resultCoins = this.coins;
      },
      error: (e:HttpErrorResponse) => this.errorService.handle(e.message)  ,
      // complete: () => console.info('complete') 
    });
  }

  addCoins(name: string, ticks: number){
    this.urlService.addCoin(name, ticks)
    .pipe(
      mergeMap(() => this.urlService.getAllCoins()),
      catchError(error => {
        this.errorService.handle(error.error);
        return throwError(() => new Error()); // return error that will trigger the subscriptions `error` block
      })
    )
    .subscribe({
      next: (result : ICoin[]) =>
      { 
        this.coins = result;
        this.resultCoins = this.coins;
      },
      error: (e:HttpErrorResponse) => this.errorService.handle(e.error)
    });
  }

  deleteCoins(id: number){
    this.urlService.deleteCoin(id)
    .pipe(
      mergeMap(() => this.urlService.getAllCoins()),
      catchError(error => {
        this.errorService.handle(error.error);
        return throwError(() => new Error()); 
      })
    )
    .subscribe({
      next: (result : ICoin[]) =>
      { 
        this.coins = result;
        this.resultCoins = this.coins;
      },
      error: (e:HttpErrorResponse) => this.errorService.handle(e.error)
    });
  }

  refreshCoins(id: number){
    this.urlService.updateCoin(id)
    .pipe(
      mergeMap(() => this.urlService.getAllCoins()),
      catchError(error => {
        this.errorService.handle(error.error);
        return throwError(() => new Error()); 
      })
    )
    .subscribe({
      next: (result : ICoin[]) =>
      { 
        this.coins = result;
        this.resultCoins = this.coins;
      },
      error: (e:HttpErrorResponse) => this.errorService.handle(e.error)
    });
  }  
  
}