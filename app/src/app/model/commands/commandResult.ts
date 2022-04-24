export class CommandResult {
    _valid: Boolean;
    _message: string;
    _data: any;

    constructor(valid: boolean, message: string, data: any) {
        this._valid = valid;
        this._message = message;
        this._data = data;
    }
}