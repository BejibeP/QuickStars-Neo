export class Joueur {
    constructor(
        public id?: number,
        public nom?: string,
        public prenom?: string,
        public mail?: string,
        public dateAjout?: Date,
        public wantModify?: boolean,
    ){}
}
