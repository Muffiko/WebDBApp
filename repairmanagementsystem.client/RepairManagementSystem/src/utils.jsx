export function formatDate(date) {
    const d = new Date(date);
    if (date === "0001-01-01T00:00:00" || isNaN(d)) return "-";
    return d.toLocaleDateString("en-GB", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
    });
}

export function formatStatus(status) {
    return (
        status
            ?.toLowerCase()
            .replace(/_/g, " ")
            .replace(/\b\w/g, (l) => l.toUpperCase()) || "Unknown"
    );
}

export const statusColors = {
    "to do": "gray",
    "in progress": "yellow",
    cancelled: "red",
    completed: "green",
    closed: "red",
};
