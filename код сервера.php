<?php

$content = file_get_contents("php://input");
$rel = '/tales/' . $_GET['taleName'] . '.zip';
file_put_contents(__DIR__ . $rel, $content);

$response = [];

if(isset($_FILES)) {
    $uploadDir = "tales/";
    $uploadFile = $uploadDir . basename($_FILES['file']['name']);
    if(move_uploaded_file($_FILES['file']['tmp_name'], $uploadFile)) {
        $response['status'] = true;
        $response['message'] = 'Успешно загружен';
        $response['link'] = 'https://nlix.ru/ArTale' . $rel;
    } else {
        $response['status'] = false;
        $response['message'] = "Ошибка ".$_FILES['file']['error'];
    }
} else {
    $response['status'] = false;
    $response['message'] = "Вы не прислали файл!";
}

echo json_encode($response, 384);