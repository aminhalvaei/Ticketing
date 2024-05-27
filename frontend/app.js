document.addEventListener('DOMContentLoaded', () => {
    const apiUrl = 'https://localhost:7169/api/Tickets';

    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            const tickets = data.$values;
            const tableBody = document.getElementById('ticketTableBody');
            tickets.forEach(ticket => {
                const row = document.createElement('tr');
                row.classList.add('cursor-pointer');
                row.innerHTML = `
                    <td class="p-4 text-right">${ticket.id}</td>
                    <td class="p-4 text-right">${ticket.userId}</td>
                    <td class="p-4 text-right">${ticket.title}</td>
                    <td class="p-4 text-right">${ticket.priorityId}</td>
                    <td class="p-4 text-right">${ticket.statusId}</td>
                `;
                row.addEventListener('click', () => openTicketModal(ticket));
                tableBody.appendChild(row);
            });
        })
        .catch(error => console.error('Error fetching tickets:', error));

    const ticketModal = document.getElementById('ticketModal');
    const ticketModalClose = document.getElementsByClassName('close')[0];
    const messagesContainer = document.getElementById('messagesContainer');
    const sendMessageButton = document.getElementById('sendMessage');
    const newMessageInput = document.getElementById('newMessage');
    const attachmentInput = document.getElementById('attachment');

    const newTicketModal = document.getElementById('newTicketModal');
    const newTicketButton = document.getElementById('newTicketButton');
    const newTicketModalClose = document.getElementById('closeNewTicketModal');
    const createTicketButton = document.getElementById('createTicket');
    const newTicketTitle = document.getElementById('newTicketTitle');
    const newTicketMessage = document.getElementById('newTicketMessage');
    const newTicketAttachment = document.getElementById('newTicketAttachment');
    const newTicketPriority = document.getElementById('newTicketPriority');

    let currentTicket = null;

    ticketModalClose.onclick = function () {
        ticketModal.style.display = 'none';
    }

    newTicketModalClose.onclick = function () {
        newTicketModal.style.display = 'none';
    }

    newTicketButton.onclick = function () {
        newTicketModal.style.display = 'block';
    }

    window.onclick = function (event) {
        if (event.target == ticketModal) {
            ticketModal.style.display = 'none';
        }
        if (event.target == newTicketModal) {
            newTicketModal.style.display = 'none';
        }
    }

    function openTicketModal(ticket) {
        currentTicket = ticket;
        messagesContainer.innerHTML = '';
        ticket.subTickets.$values.sort((a, b) => a.messageOrder - b.messageOrder).forEach(subTicket => {
            const messageDiv = document.createElement('div');
            messageDiv.classList.add('p-2', 'mb-2', 'bg-gray-100', 'rounded-lg');
            messageDiv.innerHTML = `
                <p>${subTicket.message}</p>
                ${subTicket.attachments ? `<a href="${subTicket.attachments}">Attachment</a>` : ''}
            `;
            messagesContainer.appendChild(messageDiv);
        });
        ticketModal.style.display = 'block';
    }

    sendMessageButton.onclick = function () {
        const newMessage = {
            ticketId: currentTicket.id,
            message: newMessageInput.value,
            attachments: attachmentInput.files.length ? URL.createObjectURL(attachmentInput.files[0]) : null
        };

        // Send the new message to the API (you need to implement this part)
        console.log('Sending new message:', newMessage);

        // Add the new message to the current ticket's subTickets
        currentTicket.subTickets.$values.push({
            ...newMessage,
            messageOrder: currentTicket.subTickets.$values.length + 1
        });

        // Refresh the modal
        openTicketModal(currentTicket);

        // Clear the input fields
        newMessageInput.value = '';
        attachmentInput.value = '';
    }

    createTicketButton.onclick = function () {
        const newTicket = {
            title: newTicketTitle.value,
            message: newTicketMessage.value,
            priorityId: newTicketPriority.value,
            attachments: newTicketAttachment.files.length ? URL.createObjectURL(newTicketAttachment.files[0]) : null
        };

        // Send the new ticket to the API (you need to implement this part)
        console.log('Creating new ticket:', newTicket);

        // Close the modal
        newTicketModal.style.display = 'none';

        // Clear the input fields
        newTicketTitle.value = '';
        newTicketMessage.value = '';
        newTicketAttachment.value = '';
        newTicketPriority.value = '1';
    }
});