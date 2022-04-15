export class Asset {
    public id: string;  
    public currentPrince: number;  
    public qt: number;
 
    
    constructor(id: string, currentPrince: number, qt: number){
        this.id = id;
        this.currentPrince  = currentPrince;
        this.qt = qt;
    }
}