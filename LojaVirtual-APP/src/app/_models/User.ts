import { Endereco } from './Endereco';

export interface User {
    id: number;
    nome: string;
    cpf: string;
    apelido: string;
    nascimento: Date;
    telefoneFixo: string;
    phoneNumber: string;
    receberEmail: boolean;
    receberMensagem: boolean;
    imagem: string;
    email: string;
    password: string;
    endereco: Endereco;
}
