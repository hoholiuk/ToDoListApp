export const convertDateTimeToDate = (date: string | null): string | null => {
    if(date !== null) {
        const dateFormat = new Date(date);
        const year = dateFormat.getFullYear();
        const month = (dateFormat.getMonth()  + 1).toString().padStart(2, '0');
        const day = dateFormat.getDate().toString().padStart(2, '0');

        return `${year}-${month}-${day}`;
    }

    return null;
};
