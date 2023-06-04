export const convertStringToDate = (dateString: string): Date => {
    //const [year, month, day] = dateString.split('-').map(Number);
    return new Date(dateString);
};
