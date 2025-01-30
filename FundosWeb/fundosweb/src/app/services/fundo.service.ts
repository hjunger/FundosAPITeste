import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../envinroments/environment';
import { Observable } from 'rxjs';
import { Fundo } from '../domain/Fundo';

@Injectable({
  providedIn: 'root'
})
export class FundoService {
  baseURL = environment.apiURL + 'fundo';

  constructor(private http: HttpClient) { }

  getAllFundos(): Observable<Fundo[]> {
    return this.http.get<Fundo[]>(this.baseURL)
  }
}
