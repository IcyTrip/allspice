export class Recipe{
    constructor(data) {
        this.id = data.id;
        this.title = data.title;
        this.category = data.category;
        this.img = data.img;
        this.instructions = data.instructions;
        this.creatorId = data.creatorId;
        this.favorite = false;
        this.creator = data.creator;
    }
}