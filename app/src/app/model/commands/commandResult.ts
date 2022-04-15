export class CommandResult {
    valid: Boolean;
    message: string;
    log: any;
    
    constructor(valid: Boolean, message: string, Log: any){
        this.valid = valid;
        this.message = message;
        this.log = Log;          
    }
}