import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const baseURL = 'https://globalimctaskbe20210318215446.azurewebsites.net/api/products';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient: HttpClient) { }

  readAll(): Observable<any> {
    return this.Search("");
  }

  read(id): Observable<any> {
    return this.httpClient.get(`${baseURL}/Details/${id}`);
  }

  create(data): Observable<any> {
    
    return this.httpClient.post(`${baseURL}/Create`, data);
  }

  update(id, data): Observable<any> {
    return this.httpClient.post(`${baseURL}/Edit/${id}`, data);
  }

  delete(id): Observable<any> {
    return this.httpClient.post(`${baseURL}/Delete/${id}`,null);
  }


  Search(name): Observable<any> {
    return this.httpClient.get(`${baseURL}/Search?SearchText=${name}`);
  }

  uploadImage(formData): Observable<any>{
    return this.httpClient.post(`${baseURL}/upload`, formData, {reportProgress: true, observe: 'events'})
  }
}