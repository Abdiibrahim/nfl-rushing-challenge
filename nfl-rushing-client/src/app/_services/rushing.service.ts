import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReturnData } from '../_interfaces/rushing-item';

/**
 * Api url
 */
const API_URL = 'http://localhost:5000/Rushing';

@Injectable({
  providedIn: 'root'
})
export class RushingService {

  /**
   * creates instance of rushing service
   * @param _http 
   */
  constructor(private _http: HttpClient) { }

  /**
   * Send http get request for table data
   * @param pageNum 
   * @param pageSize 
   * @param orderBy 
   * @param filter 
   * @returns Observable<HttpResponse<ReturnData>>
   */
  public getData(pageNum: number, pageSize: number, orderBy?: string, filter?: string): Observable<HttpResponse<ReturnData>> {
    let urlString = API_URL + `?pageNum=${pageNum}&pageSize=${pageSize}`;

    if (filter){
      urlString += `&filter=${filter}`;
    }
    if (orderBy) {
      urlString += `&orderBy=${orderBy}`;
    }

    return this._http.get<ReturnData>(urlString, {
      observe: 'response'
    });
  }

  /**
   * Send http get request for csv file
   * @param orderBy 
   * @param filter 
   * @returns Observable<HttpEvent<Blob>>
   */
  public exportToCsv(orderBy: string, filter: string): Observable<HttpEvent<Blob>> {
    return this._http.get(`${API_URL}/csv?orderBy=${orderBy}&filter=${filter}`, {
      responseType: 'blob',
      observe: 'response'
    });
  }
}
