import type { ITodoItem } from "./types/type";

export function GetNextId(items: ITodoItem[]):number{
    return items.reduce((max, obj) => (obj.key > max ? obj.key : max), 0) + 1;
}