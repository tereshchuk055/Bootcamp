import { Category, Todo } from "./types/type";
import { format } from 'date-fns';
import { useSelector, TypedUseSelectorHook } from 'react-redux';
import { RootState } from "./redux/store";
import moment from 'moment';


export function GetNextId<T extends Todo | Category>(items: T[]): number {
    return items.reduce((max, obj) => (obj.id > max ? obj.id : max), 0) + 1;
}

export function serializeDate(date: Date): string {
    return format(date, 'dd.MM.yyyy HH:mm');
}

export function deserializeDate(serializedDate: string): Date {
    const date = moment(serializedDate, 'DD.MM.YYYY HH:mm').toDate();
    return date
}

export const useCurrentSelector: TypedUseSelectorHook<RootState> = useSelector;