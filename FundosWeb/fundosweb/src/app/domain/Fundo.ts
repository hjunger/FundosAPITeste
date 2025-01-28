export interface Evento {
    FundoId: number;
    FundoNome: string;
    Cnpj: string;
    Administrador: string;
    DataInicio: Date
    DataFim?: Date    
  }