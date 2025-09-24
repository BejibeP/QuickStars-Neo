export class JoueurFromMatchInfo {
    constructor(
        public idJoueur?: number,
        public nom?: string,
        public prenom?: string,
        public email?: string,
        public formuleRepas?: string,
        public heureDeReponse?: Date,
        public wantModify?: boolean
    ){}
}
