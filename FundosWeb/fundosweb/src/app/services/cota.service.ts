import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../envinroments/environment';
import { Observable } from 'rxjs';
import { Cota } from '../domain/Cota';
import { Constants } from '../utils/constants';
import { DatePipe } from '@angular/common';
import { SerieCota } from '../domain/SerieCota';

@Injectable({
  providedIn: 'root'
})
export class CotaService {
  baseURL = environment.apiURL + 'CotaFundo';

  constructor(private http: HttpClient, private datePipe: DatePipe) { }

  getCotasUltimoAno(): Observable<Cota[]> {
      var dataHoje = new Date();
      var dataAnoPassado = new Date(dataHoje.getFullYear()-1, dataHoje.getMonth(), dataHoje.getDay())
      var path = this.baseURL + `/cotas/${this.datePipe.transform(dataAnoPassado, Constants.DATE_FMT_URL)}/${this.datePipe.transform(dataHoje, Constants.DATE_FMT_URL)}`
      return this.http.get<Cota[]>(path)
  }

  getSeriesCotasUltimoAno(fundoId: number): Observable<SerieCota[]> {
    var dataHoje = new Date();
    var dataAnoPassado = new Date(dataHoje.getFullYear()-1, dataHoje.getMonth(), dataHoje.getDay())
    var path = this.baseURL + `/seriesCotas/${this.datePipe.transform(dataAnoPassado, Constants.DATE_FMT_URL)}/${this.datePipe.transform(dataHoje, Constants.DATE_FMT_URL)}`
    if(fundoId > 0){
      path = path + `/${fundoId}`
    }

    return this.http.get<SerieCota[]>(path)
  } 
}
