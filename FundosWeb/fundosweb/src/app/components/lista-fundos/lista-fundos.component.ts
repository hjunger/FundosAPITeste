import { Component, input, OnInit, output, TemplateRef } from '@angular/core';
import { Fundo } from '../../domain/Fundo';
import { FundoService } from '../../services/fundo.service';
import { DateTimeFormatPipe } from "../../utils/DateTimeFormat.pipe";
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-lista-fundos',
  standalone: true,
  imports: [DateTimeFormatPipe, NgIf, NgFor],
  templateUrl: './lista-fundos.component.html',
  styleUrl: './lista-fundos.component.css',
  providers: [FundoService]
})
export class ListaFundosComponent { 
  
  public fundos: Fundo[] = []
  changeFundoId = output<number>();

  constructor(private service: FundoService){}

  public ngOnInit(): void{    
    this.carregarFundos();
  }

  private carregarFundos(): void {
    this.service.getAllFundos().subscribe(
      (fundos) => {
        this.fundos = fundos
      }
    )
  }

  alteraGraficos(fundoId: number) {
    this.changeFundoId.emit(fundoId)
  }

  cancelarFundoSelecionado() {
    this.changeFundoId.emit(0)
  }  
}
