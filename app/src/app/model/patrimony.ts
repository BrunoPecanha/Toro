export class Patrimony {
    id: number;  
    investorId: number;  
    accountAmount: number;  
    
    constructor(id: number, investorId: number, accountAmount: number){
        this.id = id;
        this.investorId = investorId;
        this.accountAmount = accountAmount;
    }
}