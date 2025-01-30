export interface Fundo {
    fundoId: number;
    fundoNome: string;
    cnpj: string;
    administrador: string;
    dataInicio: Date
    dataFim?: Date
  }