import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { debounceTime, map, Observable, retry } from 'rxjs';
 import { Constants } from './constants';
import { FullChart, ICoin, ICoinExchange } from './types';

@Injectable({
  providedIn: 'root'
})
export class URLService {

  constructor(private http:HttpClient) {}

  addCoin(name: string, ticks: number) : Observable<any> {
    var formData: any = new FormData();
    formData.append('ticks', ticks);
    return this.http.post(Constants.apiURL + 'Coin/add-coin-&-coinExchanges/' + name, formData);
  };

  getAllCoins() : Observable<ICoin[]> {
    return this.http.get<ICoin[]>(Constants.apiURL + 'Coin/get-coins-all-previous-information');
  };

  getCoinsById(formData: {id: number, step: number}) : Observable<ICoinExchange[]> {
    return this.http.post<ICoinExchange[]>(Constants.apiURL + 'Coin/get-coinExchanges', formData);
  };

  getCoinById(id: number) : Observable<FullChart> {
    return this.http.get<FullChart>(Constants.apiURL + 'Coin/get-coin-full-information-by-coin-id/' + id)
    .pipe(retry(2));
  };

  deleteCoin(id: number) : Observable<any> {
    return this.http.delete(Constants.apiURL +"Coin/delete-coin-and-coinExchanges/" + id);
  };

  updateCoin(id: number) : Observable<boolean> {
    return this.http.get<boolean>(Constants.apiURL +"Coin/update-coin-by-id-coin/" + id);
  };
}


