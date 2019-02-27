import { Product } from "./product";

export class StoreItem {
    constructor(
        public id: number,
        public product: Product,
        public quantity: number
    ) { }
}