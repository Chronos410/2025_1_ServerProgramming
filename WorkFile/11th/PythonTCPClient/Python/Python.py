import socket

SERVER_IP = "172.18.27.199"
SERVER_PORT = 9000
BUFSIZ = 512

def recvn(sock, length):
    data = b''
    while len(data) < length:
        more = sock.recv(length - len(data))
        if not more:
            break
        data += more
    return data

def main():
    try:
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

        sock.connect((SERVER_IP, SERVER_PORT))
        print(f"TCP 서버에 연결되었습니다: {SERVER_IP}:{SERVER_PORT}")

        while True:
            buf = input("\n보낼 데이터").strip()
            if not buf:
                break

            try:
                sent = sock.send(buf.encode())
            except socket.error as e:
                print(f"Send 오류: {e}")

            try:
                received = recvn(sock, sent)
                if not received:
                    break
                print(f"{len(received)} 바이트를 받았습니다.")
                print(f"받은 데이터 {received.decode()}")
            except sock.error as e:
                print(f"recv() 오류:{e}")
                break
        
    except Exception as e:
        print(f"오류발생:{e}")
    finally:
        sock.close()
        print(f"TCP 연결을 종료합니다.")

if __name__ == "__main__":
    main()