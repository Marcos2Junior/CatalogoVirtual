import { Categoria } from './Categoria';
import { Destaque } from './Destaque';

export interface Produto {
          id: number;
          nome: string;
          descricao: string;
          precoAtual: number;
          precoAntigo: number;
          descontoPorcentagem: number;
          estoque: number;
          categoria: Categoria;
          destaque: Destaque;
          ativo: boolean;
}
