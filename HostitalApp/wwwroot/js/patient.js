function confirmDelete(patientId) {
    if (confirm("Are you sure you want to delete this patient?")) {
        window.location.href = "/Patients/" + studentId + "/Delete"
    }
}