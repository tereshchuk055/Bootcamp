import { Category, Todo } from "./types/type";

export function GetNextId<T extends Todo | Category>(items: T[]):number{
    return items.reduce((max, obj) => (obj.id > max ? obj.id : max), 0) + 1;
}