function confirmDelete(doctorId) {
    if (confirm("Are you sure you want to delete this doctor?")) {
        window.location.href = "/Doctors/" + studentId + "/Delete"
    }
}